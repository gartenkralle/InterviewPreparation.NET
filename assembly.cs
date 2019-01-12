/* Strongly named assembly */

sn.exe -k c:\KeyFile.snk                        // create a key file

[assembly: AssemblyKeyFile("c:\\KeyFile.snk")]  // reference the key file inside your program

/* Global assembly cache */

gacutil.exe -i Library.dll                      // install your program into the gac

gacutil.exe -u Library                          // uninstall your program from the gac
gacutil.exe -u Library,Version=1.0.0.0,PublicKeyToken=eeaabf36d7783129

// Use it when your assembly (dll/exe) is required by more than one program
// Gac is located at: C:\Windows\Microsoft.NET\assembly\GAC_MSIL
