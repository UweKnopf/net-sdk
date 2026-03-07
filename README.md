# .net SDK for TCGDex

## This repo is under **ACTIVE DEVELOPMENT**. Breaking changes and missing functionality are expected.

This is an unofficial .net sdk for the TCGDex api. It is heavily inspired by the [official sdks](https://tcgdex.dev/sdks).

## Quickstart

### Installation

Like any other package with the dotnet cli:

<code>dotnet add package TCGDexSDK
</code>

### Usage

The main way to interact with the sdk is threw a <code>TCGDex</code> Object:

<code>// Supply a language code like in this case "en" for english for the sdk to return cards of that specified language
sdk = new TCGDex("en");
// Async fetch card based on id (in this case furret from the Darkness Ablaze Set)
var card = await sdk.FetchCard('swsh3-136');
</code>

You can also fetch sets and series based on id like this:

<code>var set = await sdk.FetchSet('swsh3');
var serie = await sdk.FetchSerie('swsh');
</code>

#### Queries

If you want to get multiple cards based on some parameters, perhaps even sorted or with pagination, you can use our query system:

<code>// Fetches all cards if they have more than 100 hp and sorts the results in descending order based on hp
var tanks = await sdk.FetchCards(new Query.Greater("hp", 100).Sort("hp", SortOrder.Descending));
</code>

#### Resumes and conversion

All cards fetched with a plural method, like <code>FetchCards</code> just above, return lists of Resumes (in this case CardResumes).
Compared to their "full" counterparts, these resumes only have a subset of properties.

Getting a "full" object from a Resume is very straight forward. To get for example a "full" card from one of the tank cards, simply write:

<code>var fullCard = await tanks[0].GetFullCard();</code>

#### Images

For images, just call the method GetImage with the quality and extension type:

<code>var imageAsBytes = fullCard.GetImage(Quality.Low, Extension.png);
//GetImage also works for Resumes
var imagesAsBytesFromResume = tanks[0].GetImage(Quality.Low, Extension.png);
</code>

*Please Note:* Because we want to be lightweight and unopinionated, we only return the images as byte arrays. 
To actually use them we recommend ImageSharp.

#### More Information

If you need more help, look at the full docs (WIP) or just open a discussion thread on the Github repo.


## Contributing

For consistency we follow the same conventions as the official project: [CONTRIBUTING.md](https://github.com/tcgdex/javascript-sdk/blob/master/CONTRIBUTING.md)

TLDR:

- Fork

- Commit your changes

- Pull Request on this Repository
