# Student Import BlobTrigger - C<span>#</span>

`BlobTrigger` that reads CSV uploaded to blob storage and automatically imports it into SQL Server. 

The records are updated in batches of X to avoid any bottle necks for lower tier SQL Instances. 

## How it works

For a `BlobTrigger` to work, you provide a path which dictates where the blobs are located inside your container, and can also help restrict the types of blobs you wish to return. For instance, you can set the path to `samples/{name}.png` to restrict the trigger to only the samples path and only blobs with ".png" at the end of their name.

## Learn more

[Azure Functions Blob Trigger Documentation](https://docs.microsoft.com/en-us/azure/azure-functions/functions-bindings-storage-blob)