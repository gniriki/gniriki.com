Published: 2016-02-06
Title: Setting up a blog with static page generator
Lead: I've decided to start a blog so I can share with the dev community. What could be better for the first post subject then setting up the blog itself? 
Author: Bartosz
---

As I started thinking about creating my own blog, the first question that popped up in my mind: where to host it? I've looked into
Azure or Amazon AWS and it became clear that the cost to run even the simplest blog on a virtual server would be a bit too much, 
especially for a site with a few posts that are mainly a static content. So I've started researching how to host my blog on cheap,
static host like S3 and stumbled into an interesting concept - Static page generators.  

One of the most important thing when running a blog (aside from writing, of course) is how hard it is to maintain it.
Wordpress and the likes of it make creating the layout and content easy and straightforward. Going full old school and creating a static page for each post
would mean a lot of work and, even more, maintenance. But what if we could have the best of both worlds? 

#### Static generators

Each time a browser requests a dynamic page, the server needs to process it (well, you can cache it, but you have to refresh it from time to time).
Load the master page and template and fill it with content. Probably ask the database for the content. Lot's of resources need to run constantly
to serve a page. Compared to that, the static generator will be run once, locally, and the generated output can be deployed to 
any hosting we like. The site won't need any further processing, which means that it will be able to live on dropbox, S3 or any other 
static (and cheap) hosting. Having no scripts to run on the server also increases the security. By a lot.  

#### Wyam

Static generators come in many types and flavors and most of interesting, open source ones
can be found on [StaticGen.com](https://www.staticgen.com/). Aside from the features, the most important thing to look at is how hard
it is to get it running and which platform does it run on. I've chosen [Wyam](http://wyam.io/), mainly because it's written it .NET and runs nicely 
on Windows. The Wyam creator's [blog](http://daveaglick.com/) is generated with it so I know it has enough features to run a neat blog. It also has a
pretty good documentation and the blog source is available at [GitHub](https://github.com/daveaglick/daveaglick), 
so it'll be easy to find out how to do things.

### Setting up the blog

#### Repository

I've chosen to make my site's source code public too, so my natural choice was the GitHub. 
I've created a [repository](https://github.com/gniriki/gniriki.com)
and choose the MIT license. You should always add a license to your public repositories so others know how and if they 
can use your code.

#### Wyam

Next step will be getting Wyam. You can go the [releases](https://github.com/Wyamio/Wyam/releases/) site and download the newest version
or add it as a [sub module](https://git-scm.com/docs/git-submodule) into your repository. I've chosen the latter, so I can update, 
build and debug it easily if needed. I've added it under `.\Wyam` and this is the path I will use in this post.
> Note: Wyam source comes with a handy build.ps1 script, but it needs Powershell 3.0 or 
> [later](https://www.microsoft.com/en-us/download/details.aspx?id=40855) to work.

Wyam comes with a few examples, including `RazorAndMarkdownBlog` which I've found has everything I need in my blog for now. 
I copied the contents of that example over to my main directory. 

Now we can run `wyam.exe`to generate our site. By default, it reads the `config.wyam` from the directory it was run from so there is no need to pass 
additional arguments. `config.wyam` contains the information about what files should be processed and how. 
If everything goes smoothly, you will have a new directory, `output`, in your main directory containing 
all of the generated files.

#### Creating the first post

So, as you've probably noticed, post files (and any text content) are of .md type, which stands for 
[Markdown](https://en.wikipedia.org/wiki/Markdown). It's a neat markup language for 
formatting plain text - for example, GitHub has the ability to read md files and even adds a readme.md when you create a repo. [Here](https://github.com/gniriki/gniriki.com/blob/master/Input/posts/Setting-up-the-blog.md) 
you can see this post on GitHub! Here is some [cheatsheet](https://github.com/adam-p/markdown-here/wiki/Markdown-Cheatsheet) 
to help you started.

You don't need a special editor for Markdown, but many have syntax coloring and preview capabilities. So open up your favorite editor
(or notepad) and create your first post in `.\Input\Posts`.

If you look at the post.cshtml template, placed in `.\input\posts\` you'll see that it uses some metadata. You can copy it from the example post and 
put it on the beginning of the post:

```
Published: 2016-02-11
Title: Setting up a blog with static pages generator
---
```

You can also create a simple BAT script which will run wyam:

```
.\Wyam\build\0.11.2-beta\bin\wyam.exe --preview --watch
```

and place it in the main directory. Those two parameters will help you work with your site. `--preview` starts a local server
which will serve our page on [localhost address](http://localhost:5080). `--watch` will keep Wyam running and any change done to the input directory or files will be automatically processed. 

If everything went all right you should have a new output file for your post, though it may not look [nice](/content/posts/first-screen.png).

#### Adding some style

Currently, the site looks like some old school page from the beginning of the 90s. Let's add bootstrap and a theme to make it nicer 
and mobile friendly. 

Get [bootstrap](http://getbootstrap.com/getting-started/#download) and unpack it to your input directory. 
Throw in the newest [jQuery 2.x](https://jquery.com/download/) to the scripts folder too.

Modify `_Layout.cshtml` so it references both bootstrap styles and scripts. 
You can see how I did it [here](https://github.com/gniriki/gniriki.com/blob/91b5ff8765a31319ba9b97cc6ff986cff10f2eb2/input/_Layout.cshtml). 
Voila, already looks a lot [better](/content/posts/bootstrap-basic.png).

Let's spice up our blog a bit with a template - I've chosen the [Clean blog](http://startbootstrap.com/template-overviews/clean-blog/). 
Download it, unpack and copy CSS, JS and Image files to your site. We'll use HTML files from the zip as an example showing how to modify our 
files. You'll need to change the `_Layout`, `home` and `post` templates. (Check [this commit on Github](https://github.com/gniriki/gniriki.com/commit/fa753ba49b37fa376adc94481da66a7ad63fd49e) 
to see how I did it).

[A lot better!](/content/posts/clean-blog-basic.png)

#### What next?

Well, the template still needs some work - links, backgrounds etc. but I leave it to you. You can check this site's 
[commit history](https://github.com/gniriki/gniriki.com/commits/master) to see how I've done it.

If you want to learn more about the Wyam itself, I recommend going to [the official site](http://wyam.io/getting-started/). 
Dave Glick's, the creator of Wyam, website can be found on [GitHub](https://github.com/daveaglick/daveaglick) to and can be
a useful source of information. When the site is ready to publish, we'll need a place to host it.
I've chosen Amazon S3 and my next post will cover that.

Cheers,
Bartosz

P. S. Don't forget to spell-check your post!