# PowerShellHandler
Ever wanted to create dynamic web pages out of PowerShell, much like is done with ASP, PHP, and Python? This is an IIS handler for PowerShell scripts allowing you to do just that

This is a very simple example of creating a handler in IIS to run PowerShell scripts instead of other server side code like ASP or PHP. The scripts should output HTML fragments in form of tabkes or custom built HTML output.

This can be used as a method of making PowerShell reports more dynamic, This could also be used to create basic dynamic websites. Dynamic in this case simply means the script runs upon recieving the reuest from IIS and executes under the IIS user context to build out the returned view.  
