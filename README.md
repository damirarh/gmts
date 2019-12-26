# GMTS - Geocaching Mystery Trail Solver

Tool for calculating final coordinates for geocaching mystery trails. Currently supported trails:

- [Pirate Cruise geocaching trails](https://www.geocaching.com/play/search?ot=4&types=8&kw=Pirate%20Cruise&c=53&utr=false) near Split, Croatia.
- [Pot miru geocaching trail](https://www.geocaching.com/play/search?ot=4&types=8&kw=Pot%20miru&owner[0]=BrdaTeam&c=181) near Nova Gorica, Slovenia

```
Usage: Gmts [options]

Options:
  -i|--input <INPUT>    GPX file with cache details
  -o|--output <OUTPUT>  CSV file with calculated coordinates
  -t|--trail <TRAIL>    Trail to calculate final coordinates for (supported: PirateCruise, PotMiru)
  -?|-h|--help          Show help information
```

Input GPX file must match the format used when exporting caches from the [geocaching.com](https://www.geocaching.com/play/search) website or the [GSAK](https://gsak.net/index.php) application.

Output CSV file contains only the GC code and calculated coordinates as required by the [ImportCorrectedCoordinates.gsk](https://gsak.net/board/index.php?showtopic=19266&st=0&#entry156236) macro for GSAK.
