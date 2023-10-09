/*
using Ajuna.NetApi;
using Ajuna.NetApi.Model.Extrinsics;
using Ajuna.NetApi.Model.Types;
using Ajuna.NetApi.Model.Types.Base;
using Ajuna.NetApi.Model.Types.Primitive;
using Schnorrkel.Keys;
using Sociali.NetApiExt.Generated;
using Sociali.NetApiExt.Generated.Model.sp_core.crypto;
using Sociali.NetApiExt.Generated.Model.sp_runtime.multiaddress;
using Sociali.NetApiExt.Generated.Storage;
using System.Threading;
*/
using UnityEngine;

public class SubstrateManager : MonoBehaviour
{
    /*
    public static SubstrateManager Instance { get; private set; }

    [SerializeField]
    public string webSocketURL = "wss://social_li_n1.wowlabz.com/";

    private SubstrateClientExt substrateClient;

    // Start is called before the first frame update
    void Start()
    {
        substrateClient = new SubstrateClientExt(new System.Uri(webSocketURL));
    }

    private void Awake()
    {
        if(Instance != null && Instance != this) 
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        } 
    }

    private void PrintBlockNumber()
    {
        var blockNumber = substrateClient.SystemStorage.Number(CancellationToken.None);
        Debug.Log("Current Block #: " + blockNumber);
    }
    */

    public async void HandleNodeConnection()
    {
        /*
        if (substrateClient != null && substrateClient.IsConnected)
        {
            await substrateClient.CloseAsync();
            Debug.Log("Disconnected from Node...");
            CancelInvoke();
        }
        else
        {
            await substrateClient.ConnectAsync();
            Debug.Log("Connected to Node...");
            InvokeRepeating("PrintBlockNumber", 0, 1f);
        }
        */
    }

    /*
    public Account GetAccount(string rawSeed)
    {
        MiniSecret miniSecret = new(Utils.HexToByteArray(rawSeed), ExpandMode.Ed25519);
        return Account.Build(KeyType.Sr25519, miniSecret.ExpandToSecret().ToBytes(), miniSecret.GetPair().Public.Key);
    }
    */

    public async void Transfer()
    {
        /*
        // 1. Create "Ajuna Test" Account object
        Account ajunaTestAccount = GetAccount("0xfb0f1a917fd2bc6c0df7fb01a046a651dee7d2802f6b9ad21247184f72281365");

        // 2. Create EnumMultiAddress object containing the destination address
        var accountId32 = new AccountId32();
        accountId32.Create("5FLSigC9HGRKVhB9FiEo4Y3koPsNmBmLJbpXg2mp1hXcS59Y");

        var multiAddressCharlie = new EnumMultiAddress();
        multiAddressCharlie.Create(MultiAddress.Id, accountId32);

        // 3. Create the transfer amount object
        var amount = new BaseCom<U128>();
        amount.Create(100000);

        // 4. Gas Fee
        var chargeAssetTx = new ChargeAssetTxPayment(0, 0);

        // 5. Calling extrinsic
        var extrinsicMethod = BalancesCalls.Transfer(multiAddressCharlie, amount);
        // var subscription = substrateClient.Author.SubmitAndWatchExtrinsicAsync(Network.ActionExtrinsicUpdate, extrinsicMethod, Network.Alice, chargeAssetTx, 64, CancellationToken.None);
        Hash hash = await substrateClient.Author.SubmitExtrinsicAsync(extrinsicMethod, ajunaTestAccount, chargeAssetTx, 64, CancellationToken.None);

        Debug.Log("Called the extrinsic. Hash: " + hash.Value.ToString());
        */
    }
}