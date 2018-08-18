# ES_MsgSystem-for-Unity
### A dialog system which supported the keywords in KrKr engine in **Unity**.

- ## **Supported keywords in texts**
    |Keywords|Commands|
    |-----------|--------|
    |lr|wait for press.|
    |w|wait for press, and erase the text.|
- ## **Call keywords commands**
    ### **Use brackets (by defult) to set the keywords commands.**
    - The brackets can be change to another characters.
    - If you want to print the brackets (or the special start command character), just repeat it to make the words not a command. 
        - ex : ( [[w] -> print "[w]" )
- ## **Use cases**
```
    1.  text:   Hello![lr]World![w]
        result: Hello!(wait for press)
        result: Hello!World!(wait for apress and erase.)

    2.  text:   Hello![w]World![w]
        result: Hello!(wait for press and erase.)
        result: World!(wait for press and erase.)
    
    3.  text:   This is [[w] and this is [[lr].[w]
        result: This is [w] and this is [lr].(wait for press and erase.)
```
