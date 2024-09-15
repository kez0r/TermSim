# **TermSim**

A simple, easy to integrate terminal-like command processor system.   

**_Includes 7 example commands:_**
* cd  
* ls  
* pwd
* ontop
* clear
* savelog
* exit

## Usage:
`<command> -<flag>:<argument>`  

### Examples: 
`$ cd -i:c:\windows`  
`$ cd -i:"c:\with spaces"`  
`$ cd -i:..`  
`$ cd -help`  
`$ ls`  
`$ pwd`    
`$ ontop`  
`$ clear`  
`$ savelog `  
`$ exit` 
  
* Commands may also be combined with pipes.

ex. `clear | pwd | cd -i:c:\windows | ls | cd -i:.. | ls`
