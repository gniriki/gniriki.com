#tool nuget:?package=Wyam
#addin nuget:?package=Cake.Wyam
#addin "Cake.AWS.S3"

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

            /*            
            var dir = new DirectoryPath("./Output/");
            var syncSettings = Context.CreateSyncSettings();
            syncSettings.Region = RegionEndpoint.USEast1;
            syncSettings.BucketName = "gniriki.com";
            //Waiting for pull request to be accepted
            syncSettings.DefaultContentType = "text/html";
            S3SyncUpload(dir, syncSettings);
            */
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