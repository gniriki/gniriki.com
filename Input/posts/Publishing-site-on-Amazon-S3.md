Published: 2016-02-11
Title: Publishing your static site on Amazon S3
Lead: So you've spent a lot of time to create a site or a blog. Now we need to publish it, so everybody can see it!
Author: Bartosz
---

In my [last post](/posts/Setting-up-the-blog) I've covered creating a simple blog with [Wyam](http://wyam.io) static generator. For a site generated like
that, a static hosting is enough. After looking at my options, I've decided to go with [Amazon S3](https://aws.amazon.com/s3/). It's cheap, has high 
availability and is free for the first year.
> Note: AWS Free Tier includes 5GB storage, 20,000 Get Requests, and 2,000 Put Requests with Amazon S3. [Here](https://aws.amazon.com/billing/new-user-faqs/)
> you can find some important information about the free tier.

### Security

The first thing I want to talk about is security. When it comes to using Amazon AWS, a breach in security can cost you a lot. 
If someone has access to your account, they can start a lot of virtual machines and use them for 
BitCoin mining or something similar and that will ramp up your bill to few thousands of dollars in a matter of hours or even minutes! 

#### Root account

You should never use your root(main) account to manage your Amazon services. Never ever. Fortunalety, Amazon let's you create additional users account, called
[IAM users](http://docs.aws.amazon.com/general/latest/gr/root-vs-iam.html). If someone gets access to your root account, you're done for, while
an IAM account can always be disabled. So the first step after creating AWS account is to
[create administrator IAM account](http://docs.aws.amazon.com/IAM/latest/UserGuide/getting-started_create-admin-group.html). Then secure your root account with 
[Multi-factor authentication](https://aws.amazon.com/iam/details/mfa/) and leave it be.
**From this point onwards don't use the root credentials unless you're in dire need!**

#### Access keys
To access your account through the web browser, you only need a username and a password. If you want to access it programmatically
(f. e. from code or from console script) you will use `access key`. **You should never place keys in your repository!** There are
web crawlers that specifically target repositories and look for access keys. Amazon does it too, so it can disable keys made public, but if
you're unlucky, it will be too late.

### Configuring Amazon Services

#### A bucket, good sir?

Amazon S3 operates on units called `buckets`. Each one of them can be used to store files and you can apply access policies to them to control who or what can read or write
to your bucket. To set up a site, you will need two of them: `domain.com` and `www.domain.com`. The first one will hold your site while the second one will just point to
the other so your site works with and without `www.`. 

Go to your [Amazon S3 console](https://console.aws.amazon.com/s3/) and create those two buckets:

![Create a bucket](/content/posts/s3-create-bucket.png)

Click on the newly created `domain.com` bucket and upload you website files.

#### Configuring your bucket
First, you'll need to make the bucket accessible for everybody for reading. From the bucket site in console switch to properties and 
chose Permissions->Add bucket policy. Paste in this policy to add a read permission - **remember to adjust domain name as needed**:

```
{
  "Version":"2012-10-17",
  "Statement":[{
    "Sid":"AddPerm",
        "Effect":"Allow",
      "Principal": "*",
      "Action":["s3:GetObject"],
      "Resource":["arn:aws:s3:::domain.com/*"
      ]
    }
  ]
}
```

Beneath the `Permissions` you'll find `Static web hosting` configuration. Enable website hosting and set Document properties accordingly:


![Static web site hosting](/content/posts/s3-static-web-hosting.png)

The last thing to do here is to config the other bucket (the one with `www.`) to redirect to your main bucket. That way, you won't need to upload your site twice.
Open the second bucket configuration and choose redirect instead of Enabling Web Hosting:

![Rediretcing bucket](/content/posts/s3-second-bucket.png)

Now you can test your site by opening `Endpoint` address at the top of `Static web hosting` section!

#### Configuring Route 53

Your site should be up and running now, but it needs a better address than `domain.com.s3-website-us-east-1.amazonaws.com` or similar. To achieve that
you need to configure Amazon Route 53 service. 

The first thing you need to set up is hosted the zone. Route 53 uses it to store information about your domain. Open up [Route 53 console](https://console.aws.amazon.com/route53/) 
nd go straight for DNS management. There, create a new hosted zone

![Create a zone](/content/posts/r53-create-zone.png)

Next, you'll create an alias for your bucket which will map your domain to the Amazon endpoint. You can do this by creating a new record set:

![Create alias](/content/posts/r53-create-alias.png)

**Remember to create an alias for each bucket (`www.domain.com` and `domain.com`)**

After you configure Route 53, the last step will be to configure Amazon as your DNS provider. This heavily depends on where you've registered your domain,
so I leave you to that. DNS values to use can be found next to newly created hosted zone:

![DNS](/content/posts/r53-dns.png)

Cheers,
Bartosz