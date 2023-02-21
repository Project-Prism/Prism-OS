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
	const releases = getDataFromApi(githubAPI+"releases");
	releases.then((result) => {
		console.log("Processing releases");
		setupReleases(result);
	});
	const contribs = getDataFromApi(githubAPI+"contributors");
	contribs.then((result) => {
		console.log("Processing contributors");
		setupContribs(result);
	});
}

function setupReleases(response)
{
	const tableBody = document.getElementById("Releases").firstElementChild.lastElementChild;
	
	for (const row of tableBody.rows)
	{
		// Get a release
		const release = response.shift();
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
}

function setupContribs(response)
{
	for (const contributor of response)
	{
		const contribData = getDataFromApi(contributor.url);
		contribData.then((result) => {processContrib(result, contributor);});
	}

	// Finally hide the placeholder and unhide real content
	document.getElementById("Wrapper").classList.remove("hidden");
	document.getElementById("ContribPlaceHolder").classList.add("hidden");
}

function processContrib(contributor, repoContrib)
{
	const wrapper = document.getElementById("Wrapper");

	const container = document.createElement("div"); container.classList.add("contributor", "innerSectionBorder");
	const pfp = document.createElement("img"); pfp.src = contributor.avatar_url;
	const profile = document.createElement("div"); profile.classList.add("profile", "maxWidth");
	const username = document.createTextNode(contributor.login);
	const profileName = document.createElement("b"); profileName.classList.add("maxWidth"); profileName.appendChild(username);
	if (contributor.name !== null)
	{
		const name = document.createElement("span"); name.appendChild(document.createTextNode(contributor.name));
		appendMultipleChildren(profileName, document.createElement("br"), name);
	}
	else
	{
		container.classList.add("noName");
	}
	const profileFollowers = document.createElement("p"); profileFollowers.appendChild(document.createTextNode(`${contributor.followers} followers · ${contributor.following} following`)); //XXX: Convert xxx,xxx (thousands) to xxxK
	const profileRepositories = document.createElement("p"); profileRepositories.appendChild(document.createTextNode(`${contributor.public_repos} repositories`));
	const profileContributions = document.createElement("p"); profileContributions.appendChild(document.createTextNode(`${repoContrib.contributions} contributions to Prism-OS`));

	// Stitch it all together
	appendMultipleChildren(profile, profileName, profileFollowers, profileRepositories, profileContributions);
	appendMultipleChildren(container, pfp, profile);
	wrapper.appendChild(container);
}

function appendMultipleChildren(Doc)
{
	for (const obj of Array.from(arguments).slice(1))
		Doc.appendChild(obj);
}

function getDataFromApi(path, useCache=true)
{
	const request = new XMLHttpRequest();
	request.open("GET", path);
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

