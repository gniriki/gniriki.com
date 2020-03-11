#tool nuget:?package=Wyam&version=1.7.4
#addin nuget:?package=Cake.Wyam&version=1.7.4

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

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
    
Task("Preview")
    .Does(() =>
    {
        Wyam(new WyamSettings
        {
            Recipe = "Blog",
            Theme = "CleanBlog",
            UpdatePackages = false,
            Preview = true,
            Watch = true
        });        
    });

Task("Deploy")
    .Does(() =>
    {
            Information("Uploading files:");
            StartProcess("aws", "s3 sync ./Output/ s3://gniriki.com --delete");
            
            Information("Removing extensions:");
            var files = GetFiles("./Output/**/*.html");
            foreach(var file in files)
            {
                    var relativePath = MakeAbsolute(Directory("./Output")).GetRelativePath(file);
                    var relativePathWithoutExtension = relativePath.ToString().Replace(".html", "");
                    Information(relativePath);
                    var parameters = "s3 mv s3://gniriki.com/" + relativePath + " s3://gniriki.com/" + relativePathWithoutExtension;
                    StartProcess("aws", parameters);
            }
    });
    
//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Preview");
    
Task("DeployToS3")
    .IsDependentOn("Build")
    .IsDependentOn("Deploy");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);