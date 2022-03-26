// See https://aka.ms/new-console-template for more information

using ClientApplicationLibrary.Network;
using System.Collections.Specialized;

// 各種.net6向け用として、用意
HttpConnectionManager HttpConnection = new HttpConnectionManager();
var host = "https://contentsroomql.azurewebsites.net";
// https://dgraph.io/docs/graphql/api/requests/#:~:text=GraphQL%20requests%20can%20be%20sent%20via%20HTTP%20POST,is%20a%20valid%20POST%20body%20for%20a%20query%3A 


// お部屋情報の取得を開始します

NameValueCollection collection = new NameValueCollection();
collection["query"] = "{room(id: 1){id name root{ pictures{ url name position{ x y z}}}}}";
collection["variables"] = "{}";
collection["operationName"] = "{}";
HttpConnection.setParameter(host, collection);
// graphqlのquery情報をPOSTにつめて実行する
var param = new Dictionary<string, object>()
{
    ["operationName"] = null,
    ["variables"] = "{}",
    ["query"] = "{room(id: 1) {id name root { pictures { url name position { x y z} } }}}",
};
      var graphqlResult = await HttpConnection.PostRequest("/api/graphql", param);
Console.WriteLine($"Hello, World! api result {graphqlResult}");
