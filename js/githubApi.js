window.onload = init;
const githubAPI = "https://api.github.com/repos/Project-Prism/Prism-OS/";

function init()
{
	console.log("Fetching from github API");
	const contribs = getDataFromApi("contributors");
	contribs.then((result) => {
		console.log(result);
	});
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
                        resolve(event.target.response);
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

