const apiUrl = "https://api.github.com/repos/Project-Prism/Prism-OS/releases"

// Get data from api
const request = new XMLHttpRequest();
request.addEventListener("load")
request.addEventListener("error")

request.open("GET", apiUrl, false)
request.setRequestHeader("Accept", "application/vnd.github+json")
XMLHttpRequest.send()

// Events
function onLoad(event)
{
    console.info("Load")
}

function onError(event)
{
    console.error("Couldn't load releases from github api")
}