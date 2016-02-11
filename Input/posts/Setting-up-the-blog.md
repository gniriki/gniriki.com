Published: 2016-02-06
Title: Setting up a blog with static pages generator
Lead: I've decided to start a blog so I can give back to the dev community. What could be better for the first post subject then setting up the blog itself? 
Author: Bartosz
---

I've always thought that dynamic pages are a bit of overkill for creating a blog or a company site. The content is pretty static 
(it's not like your posts change every 5 minutes), yet every time it's accessed, the whole, 
big, complicated machine runs and sweats just to spit out almost the same response as 10 minutes ago. 

Remember you first page? I remember mine - pure HTML, silly about me page. No PHP, no ASP, no processing, 
nothing. It was a thing of pure, beautiful, simplicity - it didn't even need a server - the browser could just read the file from my hard drive! 
And today? Simple sites need compiling, configuring, deploying and server resources to be available!

#### Static generators

All right, so I'm not saying go back to your notepad and create an HTML file for each of your post with the same header pasted in. 
We use frameworks today, so we take advantage of templating, master pages, reusing code and content, etc. This is where static generator comes in.
It will create the page for you using all of the above and more. So what's the difference? The difference is that we will run
generator once, locally, and then deploy generated output to the hosting. The site won't need any further processing, which means that 
it will be able to live on dropbox, S3 or any other static (and cheap) hosting. Having no scripts to run also increases the security by a lot.  

#### Wyam

Static generators come in many types and flavors and most of interesting, open source ones
can be found on [StaticGen.com](https://www.staticgen.com/). Aside from the features, the most important thing to look at is how hard
it is to get it running and which platform does it run on. I've chosen [Wyam](http://wyam.io/), mainly because it's written it .NET and runs nicely 
on Windows. The Wyam creator's [site and blog](http://daveaglick.com/) runs on it and it's source is available on 
[GitHub](https://github.com/daveaglick/daveaglick) so it will help a lot with setting up our own.

### Setting up the blog

#### Repository

I've chosen to make my site's source code public too, so my natural choice was the GitHub. 
I've created a [repository](https://github.com/gniriki/gniriki.com)
and choose the MIT license, I'm doing this blog so others can benefit anyway. 

#### Wyam

Next step will be getting Wyam. You can go the [releases](https://github.com/Wyamio/Wyam/releases/) site and download the newest version
or add it as a [sub module](https://git-scm.com/docs/git-submodule) in to your repository. I've chosen the second option, so I can update, 
build and debug it easily if needed. I've added it under `.\Wyam` and this is the path I will use in this post.
> Note: Wyam source comes with a handy build.ps1 script, but it needs Powershell 3.0 or 
> [later](https://www.microsoft.com/en-us/download/details.aspx?id=40855) to work.

Wyam comes with a few examples, including `RazorAndMarkdownBlog` which I've found has everything I need in my blog for now. 
I copied the contents of that example over to my main directory. 

Now we can run `wyam.exe`to generate our site. By default, it reads the config from the directory it was run from so there is no need to pass 
additional arguments. If everything went smoothly, you should have a new directory, `output` in your main directory containing 
all of the generated files.

#### Creating the first post

So, as you've probably noticed, post files (and any text content) are of .md type, which stands for 
[Markdown](https://en.wikipedia.org/wiki/Markdown). It's a neat markup language for 
formatting plain text - for example GitHub has the ability to read md files and even adds a readme.md when you create a repo. [Here](https://github.com/gniriki/gniriki.com/blob/master/Input/posts/Setting-up-the-blog.md) 
you can see this post on GitHub! Here is some [cheatsheet](https://github.com/adam-p/markdown-here/wiki/Markdown-Cheatsheet) 
to help you started.

You don't need a special editor for Markdown, but many have syntax coloring and preview capabilities. So open up your favourite editor
(or notepad) and create your first post in `.\Input\Posts`.

If you look at the post.cshtml template, placed in `.\input\posts\` you'll see that it uses some metadata. I've copied it from the example post and 
put it on the beginning of my new post:

```
Published: 2016-02-11
Title: Setting up a blog with static pages generator
---
```

I've also created a simple BAT file which runs wyam for me:

```
.\Wyam\build\0.11.2-beta\bin\wyam.exe --preview --watch
```

and placed it in the main directory. Those two parameters will help create and modify your content. `--preview` starts a local server
which will serve our page. `--watch` will keep Wyam running and any change done to the input directory or files will be automatically processed. 

If everything went all right you should have a new output file for your post, though it may not look [nice](/Content/Posts/first-screen.png).

#### Bootstrap

So, currently the site looks like some old school page from the beginning of the 90s. 
Let's add bootstrap and then some theme to make it nicer and mobile friendly. 

Get the bootstrap and unpack it to your input directory. Add the newest jQuery to the `input\scripts` and modify
_Layout.cshtml [like so](https://github.com/gniriki/gniriki.com/blob/91b5ff8765a31319ba9b97cc6ff986cff10f2eb2/input/_Layout.cshtml) 
file to include bootstrap css and scripts. Voila, already looks a lot [nicer](/Content/Posts/bootstrap-basic.png).

#### Theme - Clean blog

Let's spice up our blog a bit with a template - I've chosen the [Clean blog](http://startbootstrap.com/template-overviews/clean-blog/). 
Download it, unpack and copy CSS, JS and image files to your site. We'll use index.html from the zip to modify our files (see [this commit on Github]()).

[A lot better!](/Content/Posts/clean-blog-basic.png)

#### What next?

Well, the template still needs some work - links, backgrounds etc. but I leave it to you. You can check 
[GitHub repository](https://github.com/gniriki/gniriki.com) for this page to see how I did it. For the Wyam itself, I recommend Dave Glick's, the creator of Wyam,
[source of his blog](https://github.com/daveaglick/daveaglick). You can see there how to use Wyam and how to add some advanced 
features, like comments, to your site. When the site is ready to publish, we'll need a place to host it. I've chosen Amazon S3, but that's something for the next post.

Cheers

P. S. Don't forget to check your post with a spell-checker!