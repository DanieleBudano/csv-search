# csv-search
A program to look for a match in a csv to a specified keyword.

The compiled program is in the .\compiled-program directory. Usage:

csv-search.exe {file_path} {column_index} {search_term}


The test-batches directory contains a few batch files to quickly try different usages.
The test-file directory contains the csv file I tested this on.
The csv-search directory contains the source code.
The .csproj file contains a post-build event that automatically moves the compiled files to .\compiled-program.
I also used command line arguments within Visual Studio for faster testing, but I'm not sure if these are included in the project. Either way, it should be sufficient to run the program though any terminal, or by using the included batch files.
