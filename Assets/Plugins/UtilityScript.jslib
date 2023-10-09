mergeInto(LibraryManager.library, {
displayIframe: function (){
        document.getElementById('frame').hidden = false;
      },
leaderBoardToggle: function (){
	console.log("i ran crying..");
	updateLeaderBoard = true;	
},
gameLoaded: function(){
	isGameLoaded = true;
},
launchWowRun: function(){
  window.open("WowRun/index.html");
},
openExternalUrl: function(url){
  window.open(UTF8ToString(url));
},
updateCoinsCount: function(){
  updateCoins = true;
},
addOrDeductCoins: function(coins){
  lobbyCoins = coins;
},
displayChatToggle: function(){
        document.getElementById('chatToggleBtn').style.display = 'flex';
},
});