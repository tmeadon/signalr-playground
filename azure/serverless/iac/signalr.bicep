param location string

var name = uniqueString(resourceGroup().id)

resource signalr 'Microsoft.SignalRService/signalR@2021-10-01' = {
  name: name
  location: location
  sku: {
    name: 'Free_F1'
  }
  kind: 'SignalR'
  properties: {
    features: [
      {
        flag: 'ServiceMode'
        value: 'Serverless'
      }
    ]
  }
}

output name string = signalr.name
