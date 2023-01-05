const apiUrl = "https://api.github.com/repos/Project-Prism/Prism-OS/releases"

// Get data from api
const req = new XMLHttpRequest();
req.addEventListener("load", onLoad)
req.addEventListener("error", onError)

req.open("GET", apiUrl)
req.setRequestHeader("Accept", "application/vnd.github+json")
req.send()

// Events
function onLoad(event)
{
    // Remove event listeners!
    //request.removeEventListener("load", onLoad)
    //request.removeEventListener("error", onError)

    const releaseData = JSON.parse(event.target.responseText)
    console.info("Loaded release data")

    // Setup downloads section
    const downloads = document.getElementById("Downloads")
    const placeholder = document.getElementById("DownloadPlaceHolder")
    const releases = document.getElementById("Releases")
    const body = releases.getElementsByTagName("tbody")[0]

    // // Get the four most recent releases and create table data
    for (let i = 0; i != 4; i++)
    {
        const row = document.createElement("tr")

        // Create data fields
        const version = document.createElement("td")
        const type = document.createElement("td")
        const date = document.createElement("td")
        const download = document.createElement("td")
        const github = document.createElement("td")

        // Create text and links
        const releaseVer = document.createTextNode(releaseData[i].tag_name)
        const releaseType = document.createTextNode(releaseData[i].prerelease ? "Pre Release":"Release")
        const releaseDate = document.createTextNode(releaseData[i].published_at.split("T")[0]) // Example: 2022-08-13T21:15:55Z -> 2022-08-13

        const downloadUrl = releaseData[i].assets[0].browser_download_url
        const urlPaths = downloadUrl.split("/") // Stupid verbose variable because javascript can't do array[-1] unlike python
        const fileName = document.createTextNode(urlPaths[urlPaths.length - 1])
        const releaseDownload = document.createElement("a")
        releaseDownload.href = downloadUrl
        releaseDownload.appendChild(fileName)
        
        const tag = document.createTextNode(`tag/${releaseData[i].tag_name}`)
        const githubUrl = releaseData[i].html_url

        const releaseGithub = document.createElement("a")
        releaseGithub.href = githubUrl
        releaseGithub.appendChild(tag)

        // Link text and links to data fields
        version.appendChild(releaseVer)
        type.appendChild(releaseType)
        date.appendChild(releaseDate)
        download.appendChild(releaseDownload)
        github.appendChild(releaseGithub)

        // Link data fields to row
        row.appendChild(version)
        row.appendChild(type)
	row.appendChild(date)
        row.appendChild(download)
        row.appendChild(github)

        // Add row to table body
        body.appendChild(row)
    }

    // // Hide placeholder
    placeholder.classList.add("hidden")

    // // Unhide main table
    releases.classList.remove("hidden")
}

function onError(event)
{
    // Remove event listeners!
    request.addEventListener("load", onLoad)
    request.addEventListener("error", onError)
    
    console.error("Couldn't load releases from github api")
}
