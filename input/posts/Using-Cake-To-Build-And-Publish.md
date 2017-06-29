Published: 2017-06-29
Title: Using Cake to build and publish Wyam blog
Lead: Making life easier with Cake build
Author: Bartosz
Tags:
  - Amazon
  - S3
  - Blog
  - Cake
---

I've recently found some time to write work on the blog again, but when I wanted to publish an update, my previous deploy process broke down. While looking for some replacement I've stumbled upon Cake (C# Make).

### Building blog

To get Cake Build running you need only two files - build.ps1 and build.cake. The PowerShell script does the job of initializing everything for you and can be downloaded from [here](https://raw.githubusercontent.com/cake-build/cake/develop/build.ps1). The second one will hold our build configuration and can be created from scratch.

Fortunately for us, there is an add-in for Cake that can handle Wyam's generation. To use it, add those two lines at the beginning of you build.cake file:

```
#tool nuget:?package=Wyam
#addin nuget:?package=Cake.Wyam
```

Next, we'll define `target` argument so we'll be able to choose our task:

```
var target = Argument("target", "Build");
```

Using Cake with Wyam add-in is really simple, we just need to define a task and pass a few simple parameters. I'm using
[Wyam's Blog recipe](https://wyam.io/recipes/blog/overview) for my site,
so most of Wyam's configuration is done for me anyway:

```
Task("Build")
    .Does(() =>
    {
        Wyam(new WyamSettings
        {
            Recipe = "Blog",
            Theme = "CleanBlog",
            UpdatePackages = false
        });        
    });

RunTarget(target);
```

The last line will run our chosen task. Now, run the builds.ps1, sit back and relax. The script will download
everything you need and will run the Wyam generator.

### Deploying to S3

To deploy our site, we'll use Amazon CLI. The downloadable version uses older python, which causes some problems with S3 syncing on Windows, so I recommend getting [Python 3.6](https://www.python.org/downloads/) and installing Amazon CLI via Python's package manager: `pip install awscli'.

Next, configure AWS CLI by running `aws configure`. You'll need your Access Key Id and Secret Access key for that.

With AWS CLI ready, we can add another task to our build.cake file:

```
Task("Deploy")
    .Does(() =>
    {
            Information("Uploading files:");
            StartProcess("C:\\Users\\Bartek\\AppData\\Local\\Programs\\Python\\Python36-32\\Scripts\\aws.cmd", 
                "s3 sync ./Output/ s3://gniriki.com --delete");
    });
```

As you see here, we need to pass the full path to the aws cli to get it working. The parameters are pretty self explanatory.

It's easy to run both tasks in order by creating a third one:

```
Task("DeployToS3")
    .IsDependentOn("Build")
    .IsDependentOn("Deploy");
```

We still need to pass Target argument to the build.ps1 file. To make it easier, let's create a batch file that will do that for us. Deploy.bat:

```
@ECHO OFF
powershell -NoProfile -ExecutionPolicy Bypass -Command "& '.\build.ps1'" -Target DeployToS3
PAUSE
```

### Getting rid of .html extensions

To make our blog look more professional, we need to get rid of the .html extensions. This is a bit problematic, as S3 uses the extension to infer the content-type for the file. If you simply remove the extension and upload the file, AWS will give it default content-type (binary/octet-stream) which will break the site. We're gonna trick it by uploading the html files and removing the extensions afterward. This way files will preserve text/html type set during the upload. Let's modify the Deploy task:

```
Task("Deploy")
    .Does(() =>
    {
            Information("Uploading files:");
            StartProcess("C:\\Users\\Bartek\\AppData\\Local\\Programs\\Python\\Python36-32\\Scripts\\aws.cmd", "s3 sync ./Output/ s3://gniriki.com --delete");
            
            Information("Removing extensions:");
            var files = GetFiles("./Output/**/*.html");
            foreach(var file in files)
            {
                    var relativePath = MakeAbsolute(Directory("./Output")).GetRelativePath(file);
                    var relativePathWithoutExtension = relativePath.ToString().Replace(".html", "");
                    Information(relativePath);
                    var parameters = "s3 mv s3://gniriki.com/" + relativePath + " s3://gniriki.com/" + relativePathWithoutExtension;
                    StartProcess("C:\\Users\\Bartek\\AppData\\Local\\Programs\\Python\\Python36-32\\Scripts\\aws.cmd", parameters);
            }
    });
```

As you can see here, for each of the *.html files we run a move command which will remove the extension.

Cheers,
Bartosz