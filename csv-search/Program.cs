// Program execution starts here
// Doing various checks for command line arguments.

const int argumentsAllowedLength = 3;

if (args.Length != argumentsAllowedLength)
{
    Console.WriteLine("Usage: csv-search.exe {column_index} {search_term}");
    return;
}


// I decided to transfer the command line arguments to variables here so that if the program has to be edited to change the order or the amount of arguments...
// ...it just has to be done here instead of jumping around the code to look for every place args is used.
string filepath = args[0];
bool columnIndexIsInt = int.TryParse(args[1], out int columnIndex);
string searchTerm = args[2];
const int columnMaxIndex = 3;


if (!columnIndexIsInt)
{
    Console.WriteLine("Usage: csv-search.exe {column_index} {search_term}");
    return;
}

if (columnIndex > columnMaxIndex || columnIndex < 0)
{
    Console.WriteLine("Requested column index is too large or smaller than 0. Exiting.");
    return;
}

if (!File.Exists(filepath))
{
    Console.WriteLine("Couldn't find specified file. Exiting.");
    return;
}

try
{
    var parser = new Microsoft.VisualBasic.FileIO.TextFieldParser(filepath);
    parser.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited;
    // This commented out line was needed for the block of code below that didn't end up working
    //parser.SetDelimiters(new string[] { "," });

    // Initialize a list that will contain the output of the program, if any matching rows are found.
    List<string> rowsToOutput = new();


    // I initially had something of this sort, but...
    // ...it doesn't look like there's a method in TextFieldParse to read a row without advancing the cursor that's reading the file, so this won't work well

    //while (!parser.EndOfData)
    //{
    //    string[] currentRow = parser.ReadFields();
    //    if (currentRow[columnIndex] == search_term)
    //    {
    //        rowsToOutput.Add(currentRow);
    //    }
    //}

    // So instead we'll read the raw data of the line, and read the individual values ourselves.

    while (!parser.EndOfData)
    {
        string currentRow = parser.ReadLine();
        string[] rowData = currentRow.Split(',');
        if (rowData[columnIndex] == searchTerm)
        {
            rowsToOutput.Add(currentRow);
        }
    }

    if (rowsToOutput.Count == 0)
    {
        Console.WriteLine("No results have been found for the specified column index and search term. Exiting.");
        return;
    }

    foreach (string individualRow in rowsToOutput)
    {
        Console.WriteLine(individualRow);
    }
}
catch (Exception e)
{
    // I don't have a convenient way to test this, but I think since in a Linux environment files can lack a read permission...
    // ...this program could run into this exception if the file TextFieldParser is reading from is not readable.
    // This might also happen on Windows if running the program from a user with standard permissions, and the file is administrator only.
    Console.WriteLine("An exception occurred: " + e.ToString());
}



