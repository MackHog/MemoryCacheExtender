# MemoryCacheExtender
Extends Microsoft MemoryCache through ICache interface which expose GetAll method on the MemoryCache

## Getting Started

Download the nuget package from https://www.nuget.org/packages/MackHog.Cache.Core or clone this repo. 

## Setup

### The dip way

Enable the MemoryCacheExtender in the Startup class ConfigureServices method

```
services.AddCache();
```
This will register the ICache interface against the CacheManager which then can be injected/resolved where needed.

```
var cache = serviceProvider.GetService<ICache>();
var items = cache.GetAll();
```

Note: If you're using a different IoC tool than the build in core IoC, you need to register the ICache as a singelton resolved against an instance of CacheManager.

### The static way

The CacheManager exposes an static class called 'Cache' which can be used directly

```
var items = CacheManager.Cache.GetAll();
```
