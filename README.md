
# Rain Or Shine

A demo applicaton built with Xamarin and MVVM Cross

This application uses the OpenWeatherMap API as a datasource.

You can get a free API key by signing up here:
http://openweathermap.org/appid

Find the `ApiKeyProvider` class and paste your key into the constant marked `YOUR_KEY_HERE`.

## Building

To build or run the app, open the solution in Xamarin Studio or Visual Studio 2017.

## Unit Tests

There is a sample unit test project that demonstrates testing a component in isolation

## UI Tests

There is a sample UI test project that demonstrates black box application behaviour testing.

To run the tests:

- Open the solution in Xamarin Studio
- Select the Android project as the Startup Project
- In the Device List at the top of Xamarin Studio, choose the emulator you wish to run the tests on
- Select the "RainOrShine.UITests" project in the Solution Pad
- Go to the top menu and click Run > Run Unit Tests

The Android Application will be compiled, the UI Tests will be compiled and, finally, the tests will run. Watch the emulator.