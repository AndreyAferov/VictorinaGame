mergeInto(LibraryManager.library, {

ShowFullscreenAd: function() {
showFullScreenAdv();
},

ShowRewardedAd: function(placement) {
showRewardedAdv(placement);
return placement;
},
OpenRateUs: function(placement){
    openRateUs();
},
Authenticate: function()
{
	auth();
},
SetPlayerData: function(dataStr)
{
	setPlayerData(UTF8ToString(dataStr));
},
GetPlayerData: function()
{
	getPlayerData();
}
});