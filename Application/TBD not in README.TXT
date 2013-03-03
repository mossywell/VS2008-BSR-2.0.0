In readme, but without credits:
1. Possible customisable log location. (noomsybaby)
2. Change: allow url check to be disabled. (tubaman)

Not in readme:
1. Get stats via telnet.
2. Put DisplayCurrentStats, DisplayUsernamePassword and DisplayDetailedStats
   in their own class and make it event driven. This should get rid of all the
   callbacks.
3. Use a queue in the logmanager.
4. Change the WebPageGetter (or child class) to allow each WPG to be launched
   in a new thread rather than killing the current one and reloading it. Question:
   Should we still abort existing threads? Maybe a thread tidy up class?
5. Try to remove number crunching in the Properties of the webpagegetter class.
