mergeInto(LibraryManager.library, {
  // GetUserData: function (){
  // var returnStr = window.UserDetails;
  // var bufferSize = lengthBytesUTF8(returnStr) + 1;
  // var buffer = _malloc(bufferSize);
  // stringToUTF8(returnStr, buffer, bufferSize);
  // return buffer;
  // },

  GetUserData: function (gameObjectName, functionName) {
    console.log(`JS Lib GetUserData Log: ${UTF8ToString(gameObjectName)}, ${UTF8ToString(functionName)}`);
    window.dispatchReactUnityEvent("GetUserData", UTF8ToString(gameObjectName), UTF8ToString(functionName));
    console.log('React event dispatched successfully from GetUserData JSLIB');
  },

  GetNFTCollectionData: function (gameObjectName, functionName) {
    window.dispatchReactUnityEvent("RetrieveNFTs", UTF8ToString(gameObjectName), UTF8ToString(functionName));
  },

  BuyNFT: function (gameObjectName, functionName1, functionName2, collectionID, nftID, nftCost) {
    console.log(`JS Lib Log: ${UTF8ToString(gameObjectName)}, ${UTF8ToString(functionName1)}, ${UTF8ToString(functionName2)}, ${collectionID}, ${nftID}, ${nftCost}`);
    window.dispatchReactUnityEvent("BuyNFT", UTF8ToString(gameObjectName), UTF8ToString(functionName1), UTF8ToString(functionName2), collectionID, nftID, nftCost);
  },

  GetUserOwnedNFTCollectionData: function (gameObjectName, functionName) {
    window.dispatchReactUnityEvent("GetUserOwnedNFT", UTF8ToString(gameObjectName), UTF8ToString(functionName));
  },

  SetCurrentNFT: function (gameObjectName, functionName, collectionID, nftID) {
    window.dispatchReactUnityEvent("SetCurrentNFT", UTF8ToString(gameObjectName), UTF8ToString(functionName), collectionID, nftID);
  },

  SellNFT: function (collectionID, nftID, nftCost) {
    window.dispatchReactUnityEvent("SellNFT", collectionID, nftID, nftCost);
  },

  GetAllAvatars: function (gameObjectName, functionName) {
    window.dispatchReactUnityEvent("GetAllAvatars",  UTF8ToString(gameObjectName), UTF8ToString(functionName));
  },

  GetQuestData: function (gameObjectName, functionName) {
    console.log("gameObjectName received is:" + UTF8ToString(gameObjectName));
    console.log("functionName received is:" + UTF8ToString(functionName));
    window.dispatchReactUnityEvent("RetrieveQuests", UTF8ToString(gameObjectName), UTF8ToString(functionName));
    console.log("Dispatched Event - GetQuestData");
  },

  SetActiveAvatar: function (gameObjectName, functionName, currentAvatarUrl, worldName) {
    console.log("gameObjectName received is:" + gameObjectName);
    console.log("functionName received is:" + functionName);
    console.log("currentAvatarUrl received is:" + currentAvatarUrl);
    console.log("worldName received is:" + worldName);
    window.dispatchReactUnityEvent("SetActiveAvatar",  UTF8ToString(gameObjectName), UTF8ToString(functionName), UTF8ToString(currentAvatarUrl), UTF8ToString(worldName));
  },

  CompleteQuest: function (questId, gameObjectName, functionName){
    console.log("JSLib: Attempting to call Complete quest for Quest " + questId + "with function name: " + functionName + " being called from Game Object " + gameObjectName);
    window.dispatchReactUnityEvent("CompleteQuest", questId, UTF8ToString(gameObjectName), UTF8ToString(functionName));
  },


});
