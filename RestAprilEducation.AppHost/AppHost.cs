var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.RestAprilEducation_API>("restaprileducation-api");

builder.AddProject<Projects.RestAprilEducationRepository_RazorPages>("restaprileducationrepository-razorpages");

builder.Build().Run();
