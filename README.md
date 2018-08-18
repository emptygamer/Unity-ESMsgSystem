# ES_MsgSystem-for-Unity
### A dialog system which supported the keywords in KrKr engine in **Unity**.

- ## **Supported keywords in texts**
    |Keywords|Commands|
    |-----------|--------|
    |l|Wait for press.|
    |r|Change the line.|
    |lr|Wait for press, and change the line.|
    |w|Wait for press, and erase the text.|
- ## **Call keywords commands**
    ### **Use brackets (by defult) to set the keywords commands.**
    - The brackets can be changed to another characters.
    - If you want to print the brackets (or the special start command character), just repeat it to make the words not a command. 
        - ex : ( [[w] -> print "[w]" )
- ## **Use cases**
```
    1.  text:   Hello![l]World![w]
        result: Hello!(wait for press)
        result: Hello!World!(wait for press)

    2.  text:   Hello![r]World![w]
        result: Hello!
                World!(wait for press)

    3.  text:   Hello![lr]World![w]
        result: Hello!(wait for press)
        result: Hello!
                World!(wait for press)
    
    4.  text:   Hello![w]World![w]
        result: Hello!(wait for press)
        result: World!(wait for press)
```
