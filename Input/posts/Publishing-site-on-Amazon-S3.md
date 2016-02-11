Published: 2016-02-11
Title: Publishing your static site on Amazon S3
Lead: So you've spend a lot time to create a site or a blog. Now we need to publish it, so everybody can see it!
Author: Bartosz
---

In my [last post](/posts/Setting-up-the-blog) I've covered creating a simple blog with [Wyam](http://wyam.io) static generator. For a site generated like
that a static hosting is enough. After looking at my options I've decided to go with [Amazon S3](https://aws.amazon.com/s3/). It's cheap, has high 
availability and is free for the first year.
> Note: AWS Free Tier includes 5GB storage, 20,000 Get Requests, and 2,000 Put Requests with Amazon S3. [Here](https://aws.amazon.com/billing/new-user-faqs/)
> you can find some important information about the free tier.

### Security

The first thing I want to talk about is security. When it comes to using Amazon AWS, a breach in security can cost you a lot. 
If someone has access to your account, they can start a lot of virtual machines and use them for 
BitCoin mining or something similar and that will ramp up your bill to few thousands dollars in a matter of hours or even minutes! 

#### Root account

You should never use your root(main) account to manage your Amazon account. Never ever. Use 
[IAM user](http://docs.aws.amazon.com/general/latest/gr/root-vs-iam.html) account instead. If someone gets access to your root account, you're done for, while
an IAM account can always be disable. So the first step after creating AWS account is to
[create administrator IAM account](http://docs.aws.amazon.com/IAM/latest/UserGuide/getting-started_create-admin-group.html). Then secure your root account with 
[Multi-factor authentication](https://aws.amazon.com/iam/details/mfa/) and leave it be.
**From this point onwards don't use the root credentials unless you're in dire need!**

#### Access keys
To access your account through the web browser, you only need a user name and a password. If you want to access it programmaticaly
(f. e. from code or from console script) you will use `access key`. **You shoud never place keys in your repository!** There are
web crawlers that specifically target repositories and look for access keys. Amazon does it too, so it can disable the key in question, but if
you're unlucky, it will be to late.