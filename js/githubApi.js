window.onload = init;
const githubAPI = "https://api.github.com/repos/Project-Prism/Prism-OS/";

function init()
{
	console.log(`Inspecting cache: ${window.localStorage.length} items`);
	for (let idx = 0; idx < window.localStorage.length; idx++)
	{
		const cacheKey = window.localStorage.key(idx);
		const cacheItem = JSON.parse(window.localStorage.getItem(cacheKey));

		const date = new Date();
		if (cacheItem.expireAt === `${date.getDate()}:${date.getMonth()}`)
			window.localStorage.removeItem(cacheKey);
	}

	console.log("Fetching data");
	const releases = getDataFromApi("releases");
	releases.then((result) => {
		console.log("Processing releases");
		
		// HTML setup
		const tableBody = document.getElementById("Releases").firstElementChild.lastElementChild;
		
		for (const row of tableBody.rows)
		{
			// Get a release
			const release = result.shift();
			const version = document.createElement("td"); version.appendChild(document.createTextNode(release.tag_name));
			const type = document.createElement("td"); type.appendChild(document.createTextNode(release.prerelease ? "Pre Release":"Release"));
			const date = document.createElement("td"); date.appendChild(document.createTextNode(release.published_at.split("T")[0])); // API format: YYYY-MM-DDTHH:MM:SSZ
			let downloadUrl = "";
			// Search for the prism iso (XXX: Make a pop up window later!)
			for (const asset of release.assets)
			{
				if (asset.name.split(".").pop() === "iso")
				{
					downloadUrl = asset.browser_download_url;
				}
			}
			const downloadAnchor = document.createElement("a"); downloadAnchor.appendChild(document.createTextNode("PrismOS.iso")); downloadAnchor.href = downloadUrl;
			const download = document.createElement("td"); download.appendChild(downloadAnchor);
			appendMultipleChildren(row, version, type, date, download);
		}

		// Hide placeholder and unhide real table
		document.getElementById("DownloadPlaceHolder").classList.add("hidden");
		document.getElementById("Releases").classList.remove("hidden");
	});
}

function appendMultipleChildren(Doc)
{
	for (const obj of Array.from(arguments).slice(1))
		Doc.appendChild(obj);
}

function getDataFromApi(path, useCache=true)
{
	const request = new XMLHttpRequest();
	request.open("GET", githubAPI+path);
	request.setRequestHeader("Accept", "application/vnd.github+json");

	const getData = (resolve, reject) => {
                request.addEventListener("load", (event) => {
                        const date = new Date();
                        window.localStorage.setItem(path, JSON.stringify({content: event.target.response, expireAt: `${date.getDate()+1}:${date.getMonth()}`})); // Expire one day later, XXX: handle month limits
                        resolve(JSON.parse(event.target.response));
                }, {once: true});
                request.addEventListener("error", (event) => {
                        reject(event.target.Error);
                }, {once: true});
		request.send();
	};

	return new Promise((resolve, reject) => {
		if (useCache)
		{
			const cachedResult = window.localStorage.getItem(path);
			if (cachedResult !== null)
			{
				console.log("Using cached result");
				resolve(JSON.parse(JSON.parse(cachedResult).content));
			}
			else
			{
				console.log("Getting data from github")
				getData(resolve,reject);
			}
		}
		else
			getData(resolve, reject);
	});
}

