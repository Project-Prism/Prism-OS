<center>

<h2>Code agreement and guidelines</h2>

<hr/>

<p>⚠️ PLEASE READ BEFORE CONTINUING ⚠️</p>

<hr/>

Any code that **_you contribute_** will be under _**full
control**_ by the repository maintainers. They may change
it's licensing, the code itself, or use it for whatever
the organization sees as necesarry. This does not mean
rights, you will still have access to and retain full
ownership of your work. This agreement is in place to
prevent attempted project hijacking and legal issues.
Code taken from existing sources do not fall under
these rules, as that would be unethical and illegal.
All code taken from outside sources fall under their
original licese(s) and shall have a reference to it's
orignal license.

<hr/>

<h3>Including externaly sourced code</h3>

You are allowed to include source code from external
sources, as long as you make sure it's original license
is compatible with this project's license (GPL - V2).
It is recommended to remove the whole license copy from
the source files (if it has it) and add a comment referencing
the source's original author, source, and short name of the
original license. If the source files themselves do not have a
copy of the license within them, you must add comments showing
the original author, source, and short name of the license.

<hr/>

<h3>Spacing & Formatting</h3>

All functions, local and global variables, and
namespaces must be capitalized with the 'PascalCase'
naming Convention, and spelled properly. They
must contain professional and easy to read names,
usually they should describe what the function
does, if possible.


Variables and using statements should be clustered Like this, with descending length:

<img width="50%" alt="image" src="https://user-images.githubusercontent.com/76945439/220503038-eaef1550-a073-416f-980d-863715445ee3.png">

There shall also not be more than one class/struct/enum or namespace
in the same file.

<hr/>

Keep all code as simple as possible and make sure all code is understandable.

<hr/>

<h3>Bundling</h3>

If a class has more than one type of data, use `#region` tags like so:

<img width="50%" alt="image" src="https://user-images.githubusercontent.com/76945439/220502977-54f45a7f-5947-41a4-ac68-18a73f631a7c.png">

<hr/>

Do NOT create hacky methods to get around something unless you know you will change it before you commit. You _will_ regret making something very unreliable then having to re-make it for several hours in a more stable manner)

<hr/>

Always use IF statments "properly" and clearly. and example is if you have many nested IFs, invert the condition and move it to the top, and return it if the condition is met.

<hr/>

Don't define namespaces within namespaces, and always include only one namespace definition _per file_, just below the using statements. They should not use brackets like so:
```cs
namespace PrismOS;
```
  
<hr/>

  <h3>Licensing</h3>

You must include files from other projects (with compatible licenses) like so:
![image](https://github.com/Project-Prism/Prism-OS/assets/76945439/e182ab0d-2643-4215-a072-605f0954e49c)

</center>
