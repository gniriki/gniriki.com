Published: 2016-02-06
Title: Setting up a blog with static pages generator
Lead: I've decided to start a blog so I can share back knowledge about dev stuff instead only relying on others.
Author: Bartosz
---
I've decided to start a blog so I can share back knowledge about dev stuff 
instead only relying on others. What could be a better subject for a first 
post than setting up my blog itself? 
I'm actually writing this as I set up my blog, so those first lines 
are written as plain text in Notepad++!

### Technology and platform

I've always thought that dynamic pages are a bit of overkill for a 
blog or a game dev company page. The content is pretty static, yet 
every time someone access it, the server needs to process the whole thing. 
Remember you first page? I remember mine - pure HTML, silly about 
me page. No PHP, no ASP, no processing, nothing. I didn't even need a server - the browser just read the file from my hard drive! 
Today we need a bit more to make a nice page and dynamic pages are 
usually the way to go. 

#### Enter StaticGen

The static generator does similar stuff as the dynamic page web server, but 
the main difference is, they do it only once and locally. The output 
from StaticGen is just raw HTML with some CSS files and maybe a js here and there. 
It doesn't need any more processing. You can open the site from your 
hard drive or host it on drop box. They are secure (there is no 
server running scripts) and lightweight. 

#### Wyam
There are many StaticGens today, most of worthy open source ones
can be found on [Static gen site](https://www.staticgen.com/). I chose [Wyam](http://wyam.io/)
because I'm a .NET dev so I can fiddle with code is needed and it will be working nicely on Windows, 
compared to some others that need a lot of configuration to get 
them running on Windows. I need something that is easy to set up and 
run, so I won't be discouraged from writing by tools I use. 

### Setting up my site

#### Repository

Firstly, I will need some code repo to store my site. My site is public, 
so why not the source code? Let's go with GitHub and an MIT license. 
It's public anyway and I'm publishing it on GitHub so others can benefit.
(You can read more about licenses here [Choose a license](http://choosealicense.com/)).
A quick clone, add, commit and push and my current version of this 
post is on the [Github](https://github.com/gniriki/gniriki.com). Awesome!

#### Wyam

Next step will be getting wyam. It can be downloaded and run as a 
standalone app, but as it is hosted on GitHub too, I'll just add a submodule link in my repository 
so I'm always up to date - I've added it under `.\Wyam`. 
(Blah, it seems that my Git client is outdated and I need to upgrade 
before adding a submodule...). All right, done.

Wyam comes with a few examples, including `RazorAndMarkdownBlog` which 
I've decided to use. I copied it over to my main folder and moved my post file to the 
`.\Input\Posts` folder. 

First we need to build the Wyam itself. Fortunately, it comes with nice 
PowerShell script that does everything for us - `build.ps1` located 
in the main Wyam directory. Unfortunately, I have old Powershell and it needs at least 3.0. 
I've installed 4.0 from [here](https://www.microsoft.com/en-us/download/details.aspx?id=40855)
(Windows6.1-KB2819745-x64-MultiPkg.msu for my x64 Windows 7). 
Mind you that Windows 8.1 and newer has this built-in.
If everything went smoothly, you should have a working `wyam.exe`
in 
```
.\Wyam\build\VERSION\bin
```
folder. Let's use it to generate this site. 
In the main folder, I've created a script that will make it easier to run Wyam in future.
I've used parameters from Wyam readme:

```
.\Wyam\build\0.11.2-beta\bin\wyam.exe --preview --watch
```

and got an error! It seems that my post is missing some metadata. 
I've copied this metadata from the example post and put it on the beginning of my post file:

```
Published: 2015-04-25
Title: Setting up a site and blog with static pages generator
---
```

And... it work! Though it looks like [crap](/Content/Posts/first-screen.png) - 
to make screens work I needed to add this step in config.wyam to copy static files:

```
Pipelines.Add("Resources",
    CopyFiles("*").WithoutExtensions(".cshtml", ".md", ".less")
);
```

#### Markdown

So, as you've probably noticed, post files are of .md type, which stands for 
[Markdown](https://en.wikipedia.org/wiki/Markdown). It's a neat language for 
formatting plain text - for example, GitHub adds a readme.md when you create a repo and uses it
to display `md` files in a repo. [Here](https://github.com/gniriki/gniriki.com/blob/master/Input/posts/Setting-up-the-blog.md) 
you can see this post on GitHub!
Here is some [cheatsheet](https://github.com/adam-p/markdown-here/wiki/Markdown-Cheatsheet) 
to help you started.

Those two parameters we used to start Wyam will be helpful now. `--preview` starts a local server
which will serve your page. `--watch` will keep Wyam running and any change done 
to the post will be automatically processed. So, set up your screen like [that](/Content/Posts/side-by-side.png) or use
two monitors and make your post more readable! BTW - one line break 
doesn't mean anything in markdown so you can use that to format your source text file!

#### Bootstrap

So, the site currently looks like some old school page from the beginning of the 90s. 
Let's add bootstrap and then some theme to make it nicer and mobile friendly. 

Get the bootstrap and unpack it to your input folder. Add the newest jQuery to the `input\scripts` and modify
_Layout.cshtml [like so](https://github.com/gniriki/gniriki.com/blob/91b5ff8765a31319ba9b97cc6ff986cff10f2eb2/input/_Layout.cshtml) 
file to include bootstrap css and scripts. Voila, already looks a lot [nicer](/Content/Posts/bootstrap-basic.png).

#### Clean blog

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