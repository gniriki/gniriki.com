Published: 2016-02-06
Title: Setting up a site and blog with static pages generator
---
I've decided to start a blog so I can share back knowledge about dev stuff 
instead only relying on others. What could be better subject for a first 
post than setting up my blog itselft? 
I'm actually writing this as I set up my blog, so those first lines 
are written as plain text in Notepad++!

### Technology and platform

I've always thought that dynamic pages are a bit of overkill for a 
blog or a game dev company page. The content is pretty static, yet 
every time someone access it, the server needs to process the whole thing. 
Remember you first page? I remember mine - pure html, silly about 
me page. No PHP, no ASP, no processing, nothing. I didn't even need a server - the browser just read the file from my hard drive! 
Today we need a bit more to make a nice page and dynamic pages are 
usually the way to go. 

#### Enter StaticGen

Static generators do similar stuff as dynamic page web server, but 
the main difference is, they do it only once and locally. The output 
from StaticGen is just raw htmls with some css files and maybe a js here and there. 
It doesn't need any more processing. You can open the site from your 
hard drive or serve from drop box. They are secure (there is no 
server running scripts) and lightweight. 

#### Wyam
There are many StaticGens today, most of worthy open source ones
can be found on [Static gen site](https://www.staticgen.com/). I chose [Wyam](http://wyam.io/)
because I'm a .NET dev so I can fiddle with code is needed and it will be working nicely on Windows, 
compared to some others that needs a lot of configuration to get 
them running on Windows. I need something that is easy to set up and 
run, so I won't be discouraged from writing by tools I use. 

### Setting up my site

#### Repository

Firstly, I will need some code repo to store my site. My site is public, 
so why not the source code? Let's go with github and a MIT licence. 
It's public anyway and I'm publishing it on github so others can benefit.
(You can read more about licenses here [Choose a license](http://choosealicense.com/)).
A quick clone, add, commit and push and my current version of this 
post is on the [Github](https://github.com/gniriki/gniriki.com). Awesome!

#### Wyam

Next step will be getting wyam. It can be downloaded and run as a 
standalone app, but as it is hosted on github too, I'll just add a submodule link in my repository 
so I'm always up to date - I've added it under `.\Wyam`. 
(Blah, it seems that my Git client is outdated and I need to upgrade 
before adding a submodule...). All right, done.

Wyam comes with a few examples, including `RazorAndMarkdownBlog` which 
I've decided to use. I copied it over to my main folder and moved my post file to the 
`.\Input\Posts` folder. 

First we need to build the Wyam itself. Fortunately it comes with nice 
powershell script that does everything for us - `build.ps1` located 
in the main Wyam directory. Unfortunalety I have old Powershell and it needs at least 3.0. 
I've installed 4.0 form [here](https://www.microsoft.com/en-us/download/details.aspx?id=40855)
(Windows6.1-KB2819745-x64-MultiPkg.msu for my x64 Windows 7). 
Mind you that Windows 8.1 and newer has this built-in.
If everything went smoothly, you should have a working `wyam.exe`
in 
```
.\Wyam\build\VERSION\bin
```
folder. Let's use it to generate this site. 
In the main folder I've created a script that will make it easier to run Wyam in future.
I've used parameters from Wyam readme:

```
.\Wyam\build\0.11.2-beta\bin\wyam.exe --preview --watch
```

and got an error! It seems thay my post is missing some metadata. 
I've copied this metadata from the example post and put it on the beggining of my post file:

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
formatting plain text - for example, GitHub adds a readnme.md when you create a repo and uses it
to display `md` files in repos. [Here](https://github.com/gniriki/gniriki.com/blob/master/Input/posts/Setting-up-the-blog.md) 
you can see this post on github!
Here is some [cheatsheet](https://github.com/adam-p/markdown-here/wiki/Markdown-Cheatsheet) 
to help you started.

Those two parameters we used to start Wyam will be helpfull now. `--preview` starts a local server
which will serve your page. `--watch` will keep Wyam running and any change done 
to the post will be automaticly processed. So, set up your screen like [that](/Content/Posts/side-by-side.png) or use
two monitors and make your post more readable! BTW - one line break 
doesn't mean anything in markdown so you can use that to format your source text file!

#### Bootstrap

So, the site currently looks like crap. Let's add bootstrap and then some theme to make it
nice and responsive. 