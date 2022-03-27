// See https://aka.ms/new-console-template for more information

using ClientApplicationLibrary.Network;
using System.Collections.Specialized;

// 各種.net6向け用として、用意
HttpConnectionManager HttpConnection = new HttpConnectionManager();
var host = "https://contentsroomql.azurewebsites.net";
// https://dgraph.io/docs/graphql/api/requests/#:~:text=GraphQL%20requests%20can%20be%20sent%20via%20HTTP%20POST,is%20a%20valid%20POST%20body%20for%20a%20query%3A 

// 開通されているお部屋一覧情報を取得します。
Console.WriteLine("指定したお部屋一覧情報を取得します。");
Thread.Sleep(2000);

NameValueCollection collection = new NameValueCollection();
collection["query"] = "{room(id: 1){id name root{ pictures{ url name position{ x y z}}}}}";
collection["variables"] = "{}";
collection["operationName"] = "{}";
HttpConnection.setParameter(host, collection);

// graphqlのquery情報をPOSTにつめて実行する
var roomsParam = new Dictionary<string, object>()
{
    ["operationName"] = null,
    ["variables"] = "{}",
    ["query"] = "{rooms{name id}}",
};
var roomsResult = await HttpConnection.PostRequest("/api/graphql", roomsParam);

Console.WriteLine(roomsResult);
var isLoop = true;
while (isLoop) {

    Console.WriteLine("詳細確認をしたい部屋のidを入力してください");

    var input = Console.ReadLine();

    // お部屋情報の取得を開始します
    Console.WriteLine("指定したお部屋 の情報を取得開始します。");
    Thread.Sleep(2000);
    // graphqlのquery情報をPOSTにつめて実行する
    var roomDetailParams = new Dictionary<string, object>()
    {
        ["operationName"] = null,
        ["variables"] = "{}",
        ["query"] = "{room(id:" + input + " ) {id name root { pictures { url name position { x y z} } }}}",
    };
    var roomDetailResult = await HttpConnection.PostRequest("/api/graphql", roomDetailParams);
    Console.WriteLine($"お部屋内のお絵描き情報を取得しました。 {roomDetailResult}");

    Console.WriteLine("終わりたい場合は2をもう一度検索したい場合は、他のボタンを押してください。");

    var mode = Console.ReadLine();

    if (mode == "2") {
        break;
    }


}

Console.WriteLine("お部屋の検索を終了します");

