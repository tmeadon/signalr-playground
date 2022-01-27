param baseName string
param location string = 'uksouth'
param signalRName string

var uniqueName = uniqueString(resourceGroup().id)

resource stg 'Microsoft.Storage/storageAccounts@2019-06-01' = {
  name: uniqueName
  location: location 
  sku: {
    name: 'Standard_LRS'
  }
  kind: 'StorageV2'
}

resource asp 'Microsoft.Web/serverfarms@2019-08-01' = {
  name: baseName
  location: location
  sku: {
    name: 'Y1'
    tier: 'Dynamic'
  }
}

resource ai 'Microsoft.Insights/components@2015-05-01' = {
  name: baseName
  location: location
  kind: 'web'
  properties: {
    Application_Type: 'web'
  }
}

resource signalr 'Microsoft.SignalRService/signalR@2021-10-01' existing = {
  name: signalRName
}

resource fa 'Microsoft.Web/sites@2019-08-01' = {
  name: uniqueName
  location: location
  identity: {
    type: 'SystemAssigned'
  }
  properties: {
    serverFarmId: asp.id
  }
  kind: 'functionapp'

  resource appSettings 'config@2018-11-01' = {
    name: 'appsettings'
    properties: {
      'AzureWebJobsStorage__accountName': stg.name
      'FUNCTIONS_WORKER_RUNTIME': 'dotnet-isolated'
      'APPINSIGHTS_INSTRUMENTATIONKEY': ai.properties.InstrumentationKey
      'AzureSignalRConnectionString': signalr.listKeys().primaryConnectionString
    }
  }
}

resource blobContrib 'Microsoft.Authorization/roleAssignments@2020-04-01-preview' = {
  name: guid(fa.name, stg.name, 'ba92f5b4-2d11-453d-a403-e96b0029c9fe')
  properties: {
    principalId: fa.identity.principalId
    roleDefinitionId: resourceId('Microsoft.Authorization/roleDefinitions', 'ba92f5b4-2d11-453d-a403-e96b0029c9fe')
    principalType: 'ServicePrincipal'
  }
  scope: stg
}

resource queueContrib 'Microsoft.Authorization/roleAssignments@2020-04-01-preview' = {
  name: guid(fa.name, stg.name, '974c5e8b-45b9-4653-ba55-5f855dd0fb88')
  properties: {
    principalId: fa.identity.principalId
    roleDefinitionId: resourceId('Microsoft.Authorization/roleDefinitions', '974c5e8b-45b9-4653-ba55-5f855dd0fb88')
    principalType: 'ServicePrincipal'
  }
  scope: stg  
}
