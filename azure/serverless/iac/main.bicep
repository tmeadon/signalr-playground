targetScope = 'subscription'

var baseName = 'signalr'
var location = 'uksouth'

resource rg 'Microsoft.Resources/resourceGroups@2021-04-01' = {
  name: baseName 
  location: location
}

module signalr 'signalr.bicep' = {
  scope: rg
  name: 'signalr'
  params: {
    location: location
  }
}

module functionApp 'functionApp.bicep' = {
  scope: rg
  name: 'functionApp'
  params: {
    baseName: baseName
    location: location
    signalRName: signalr.outputs.name
  }
}
