Setting up a site and blog with static pages generator

I've decided to start a blog so I can share back knowledge about dev stuff instead only relying on others. What could be better subject for a first post than setting up my blog itselft? 
I'm actually writing this as I set up my blog, so those first lines are written as plain text in Notepad++!

Technology and platform

I've always thought that dynamic pages are a bit of overkill for a blog or a game dev company page. The content is pretty static, yet every time someone access it, the server needs to process the whole thing. 
Remember you first page? I remember mine - pure html, silly about me page. No PHP, no ASP, no processing, nothing. I didn't even need a server - the browser just read the file from my hard drive! 
Today we need a bit more to make a nice page and dynamic pages are usually the way to go. 

Enter StaticGen

Static generators do similar stuff as dynamic page web server, but the main difference is, they do it only once and locally. The output from StaticGen is just raw htmls with some css files and maybe a js here and there. 
It doesn't need any more processing. You can open the site from your hard drive or serve from drop box. They are secure (there is no server running scripts) and lightweight. 

Wyam
There are many StaticGens today, most of worthy open source nes can be found on https://www.staticgen.com/. I chose http://wyam.io/ because I'm a .NET dev so I can fiddle with code is needed and it will be working nicely on Windows, 
compared to some others that needs a lot of configuration to get them running on Windows. I need something that is easy to set up and run, so I won't be discouraged from writing by tools I use. 

Setting up my site

Repository

Firstly, I will need some code repo to store my site. My site is public, so why not the source code? Let's go with github and a MIT licence. It's public anyway and I'm publishing it on github so others can benefit.
(You can read more about licenses here http://choosealicense.com/). A quick clone, add, commit and push and my current version of this post is on the https://github.com/gniriki/gniriki.com. Awesome!
Next step will be getting wyam. It can be downloaded and run as a standalone app, but as it is hosted on github too, I'll just add a link in my repository so I'm always up to date. 
(Blah, it seems that my Git client is outdated and I need to upgrade before adding a submodule...). All right, done. Let's try to generate my site!

Wyam

First we need to build the Wyam itself. Fortunately it comes with nice powershell script that does everything for us. Unfortunalety I have old Powershell and it needs at least 3.0. 
I've installed 4.0 form here https://www.microsoft.com/en-us/download/details.aspx?id=40855 (Windows6.1-KB2819745-x64-MultiPkg.msu for my x64 Windows 7). Windows 8.1 and newer have this built-in.
Wyam comes with a few examples, including RazorAndMarkdownBlog which I've decided to use. I copied it over to my main folder and moved my post file to the input/Posts folder. 
