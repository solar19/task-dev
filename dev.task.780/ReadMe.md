# Development test in C#

## Description

Please write a console application to read some image's URLs from the attached `links.txt` file, 
save them on local disk, and meanwhile save these information about each link/file in a datastore of your choice:
```{
    URL,
    LocalName,
    FileExtension,
    FileSize,
    DownloadDate // in UTC
}```

## Alternative Method
For this assessment, we can use database to save file as base64 with other information. Actually the decision between saving in file or saving as base 64 depends on your requirements.
so, there is no best practice for using database or disk for saving image