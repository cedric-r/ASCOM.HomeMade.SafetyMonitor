ASCOM.HomeMade.SafetyMonitor is a SafetyMonitor and ObservingConditions (weather) ASCOM driver.

It reads its data from a Lunatico SOLO connected to a CloudWatcher, any network source that produces data in the same format, or even a local text file.

The SafetyMonitor part does the following checks:
- Check that internet connection is working (optional).
- Check that the system isn't running on USP (optional).
- Check that the ambian temperature isn't above or below certain limits.
- Check that ambient humidity isn't above or below certain limits.
- Check that the sky temperature is below a certain threshold.
- Check that the wind and wind gusts aren't above a certain threshold.
- Check that the luminosity is below a certain level (optional).

This software ien't fully tested yet, although I've been using it on two setups for a few years.
