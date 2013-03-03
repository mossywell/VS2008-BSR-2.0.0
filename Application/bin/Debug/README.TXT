Mossywell's BSR README.TXT
========================================
Latest version: 2.0.0.0 (14 August 2008)


Versions History:
=================
- 2.0.0.0 (14 August 2008)
  1.  Change: Changed the name from Bounce Sky Router to BSR. Bounce Sky Router
      was getting too much to say! Also, Non-Sky users were also using this app
      so the word "Sky" has become obsolete. (A few of the dialogs in the help
      file still say "Bounce Sky Router". I hope you'll understand!)
  2.  Fix: Fixed the Add / Remove Programs icon which wasn't showing the 
      transparency.
  3.  Change: Changed the code to work with the latest version of the Sky
      firmware, V1.03.87. Note that it hasn't been tested against the older
      firmware versions because Sky will have updated them to the newest
      version.

- 1.9.0.0 (23 August 2007)
  1.  Fix: Corrected the Help file that got the received and transmitted values
      in the log file the wrong way round!
  2.  Change: Did the third major restructuring as described in the version
      1.8.0.0 changes.
  3.  New: Added a comment to the Help file regarding configuration of Bounce
      Sky Router when downloading torrents.
  4.  New: Option to disable the automatic web checking. If set, this means that
      the reboots will only happen automatically based on the SNR dropping below
      the threshold.
  5.  Change: Bounce Sky Router no longer sends any commands to the router
      if it deems that the router is not contactable or that another
      Administrator is connected to the router. (In previous versions, it tried
      to send the command anyway.)
  6.  Fix: Corrected one of the warnings when the default gateway could not
      be ascertained.
  7.  Change: The Help file has been updated with the latest information.
  8.  Change: Prioritorised the planned changes in this readme file.
  9.  Fix: Fixed the problem whereby it was possible for a reboot to be sent
      immediately after resuming from a power mode suspend.
  10. New: Added confirmation question when the "Run Custom Commands Now" menu
      option is selected.
  11. New: Added a new menu option to put Bounce Sky Router into maintenance
      mode. This is where the whole application  runs as usual except that the
      auto-reboot functionality is disabled. This is a useful mode when there
      is router maintenance happening but you don't want to put the whole
      application on pause.
  12. Fix: Fixed a (very unlikely) bug whereby it was possible for a reboot
      to happen immediate after a reboot cycle had completed.
  13. Change: Increased the default height of the log reader.
  14. Fix: Fixed a problem of the web page time equaling 0, which is a failure,
      but Bounce Sky Router interpreting it as a success.
  15. Fix: On Vista, there was a long delay in reconnecting to the Internet
      after a reboot. This was caused by the way the IP stack in Vista works.
      Bounce Sky Router now detects this and the long delay has been resolved.
  16. New: Added a list of firmwares to the Help file that Bounce Sky Router
      has been tested against.
  
- 1.8.0.0 (01 July 2007)
  NOTE: This version is a _major_ change in terms of the internal structure
  of the code. (Indeed, more so than changes in functionality.) The purpose
  of these changes is to make the code more modular which will make future
  changes much easier. There are three major internal areas of change and
  this release has done two of them. (The third is independent of the first
  two.) I have decided to release this version at this point, for now, to
  ensure that I haven't written any bugs into this release (and to make
  debugging easier if I have)! Once all is well, I'll do the third major
  internal change. Note that this restructuring will not actually result in
  any obvious visible difference in the way Bounce Sky Router functions. 
  1.  Change: Restructured some of the internal code to be stateful.
  2.  Change: Communication with the worker threads is now done using event
      subscriptions.
  3.  Change: Changed the tray icon text at startup to "Loading...".
  4.  Change: HTTP Keep-Alive is explicitly turned off.
  5.  Fix: Web timings now only take account of the web time and not also the
      additional code time. NOTE: This shortens the timings by quite a bit.
  6.  Fix: Certain menu options are now unavailable when BounceSkyRouter is
      paused.
  7.  Change: Swapped over the first and second URLs (to show that it is not
      always the latter that is slowest - though there is still a small
      difference between the two due to the way the .Net garbage collector
      works).
  8.  Change: After a manual reboot, the custom commands, if there are
      any are run automatically.
  9.  New: If any exceptions manage to bubble their way up to Main, the
      exception is now logged to a separate file: 
      "Mossywell.BSR.Exception.log".
  10. New: There is a new config file option "auto_version_check" that, if set
      to "False", disables the application version checking.
  11. Change: Changed the menu order slightly.

- 1.7.1.0 (02 June 2007)
  1.  Fix: Fixed the loading of the readme.txt (both at install time and from
      the menu), the .... when Bounce Sky Router is not installed to the
      default location of "C:\Program Files".
  2.  New: The "Display ADSL Username and Password" dialog box now has a "Copy
      to Clipboard" button.

- 1.7.0.0 (11 May 2007)
  1.  New: Improved error notification when an internal critical error occurs.
  2.  Change: Yet another method of calculating the router IP address! This one
      doesn't rely on WMI, so it works on Windows 2000. It also understands
      multiple default gateways and now selects the one with the lowest metric
      but excludes routes of type PPP (e.g. excludes VPN connections). Finally,
      it has been tested successfully in Vista.
  3.  Change: No longer logs the various settings at close.
  4.  Fix: Fixed the bug whereby if the current working directory is changed in
      the application shortcut, the settings file, the reboot wav and the help
      files are not found. This fix allows Bounce Sky Router to be launched from
      Vista's App Launcher gadget. Also, the About form now shows the correct
      executable location (and is big enough to show the path in most cases!).
  5.  Change: Increased the maximum startup delay from 30 to 90 seconds.

- 1.6.1.0 (11 April 2007)
  1.  Fix: Minor typo in one of the errors.
  2.  Change: The Help file now explicitly states that this app is only
      designed to work on the Netgear DG834GT router running Sky's firmware.
  3.  New: Added a menu option to display the readme (this file).

- 1.6.0.0 (29 March 2007)
  1.  Fix: Changed to stricter data type marshalling to unmanaged code for 64
      bit platforms.
  2.  Change: Double-clicking on the icon now pauses Bounce Sky Router instead
      of loading the log file. (The log file can still be loaded via the
      menus.) Double-clicking again restarts it.
  3.  New: There is a new menu option to pause and re-start Bounce Sky Router.
  4.  New: Added a new config file option. The option defines the increased
      frequency with which the application attempts to connect to the Internet
      whilst it is rebooting. The purpose of this additional option is to speed
      up the reboot cycle. At best, it can reduce the reboot cycle time by
      about 40 seconds (assuming that there are custom commands). At worst, it
      won't make any difference.
  5.  Fix: Changed the way (again) that the default gateway is calculated.
      Hopefully, this time, I've got it working in all the weird and wonderful
      configurations that some people have!
  6.  New: Added a new config file option to cause Bounce Sky Router to pause
      its own execution for a user-definable time at startup. This
      option is useful when the network isn't fully ready by the time the
      application loads (as can happen with wireless connections).
  7.  New: Added a new config file option to reboot the router when the SNR
      drops below a user definable threshold. If the option is empty, zero
      (the default) or gobbledygook, the threshold reboot functionality is
      disabled (i.e. it behaves like all the older versions).
  8.  Change: Mopped up a small number of typos!

- 1.5.0.0 (05 February 2007)
  1.  New: A new user option "max_logfile_count" has been added. This is the
      maximum number of log files that can exist. Log files are deleted
      starting with the oldest first, and the deletion (called "pruning")
      is logged in the current day's log. 
  2.  Change: Log files are now located in the "C:\Documents and Settings
      \<user>\My Documents\Mossywell\Bounce Sky Router" directory instead of
      the "Program Files\Mossywell\Bounce Sky Router" directory. This is
      because not all users have write access to the "Program Files"
      directory and Microsoft are pushing for all user writable data to be
      written outside of the "Program Files" (typically the user's profile
      directory). This is particularly true in Vista.
  3.  Fix: If Bounce Sky Router is started with a Microsoft VPN client running,
      it no longer treats the remote router as the Sky router. This is
      even if the "Use default gateway on remote network"  option is selected. 
  4.  New: The amount of logging is now customisable. The default (6) actually
      gives the same level of logging as previous versions.
  5.  Change: The temporary file created when viewing the stats web page is now
      created in the same directory as the log files instead of "Program
      Files".
  6.  Change: Some of the menu items have more meaningful names.
  7.  New: New option to show the detailed router stats , which are often
      requested on www.skyuser.co.uk for router speed diagnostics.

- 1.4.0.0 (23 January 2007)
  1.  New: A new custom-written log file viewer that auto-updates so that
      you no longer have to keep re-loading the log file.
  2.  New: Bounce Sky Router now uses the system uptime to detect whether the
      attempt to reboot the router was a success or failed, and logs
      accordingly.
  3.  New: Bounce Sky Router now detects whether the failure to contact the
      router is a result of the router already being logged in to at another
      IP address (as might happen if Bounce Sky Router is already running on
      the same network.)
  4.  New: Double-clicking on the system tray icon now opens up the log viewer.

- 1.3.0.0 (19 January 2007)
  1.  New: Made the web site check and stats check intervals user-definable.
      These can now be accessed in the config file. They still default to the
      old values of 30 and 15 seconds respectively.
  2.  New: Made the web site check and stats check timeouts user-definable.
      These can now be accessed in the config file. They still default to the
      old values of 5 seconds.
  3.  New: Start message in the log file now contains the version and the
      values of some of the user-defined settings. (This is to show whether the
      values used are indeed valid and if not, which ones will be used
      instead.)
  4.  Change: Bounce Sky Router now no longer writes to the event log. Instead,
      it only writes to the log file (if necessary).
  5.  New: Logging now shows when Bounce Sky Router is closed.
  6.  Change: If Bounce Sky Router is unable to write to the log file, it now
      closes down gracefully (i.e. without an exception). (It still has to
      close, however!)
  7.  New: The log file now shows the uptime, transmit packets and receive
      bytes since last reboot in addition to the existing downstream SNR and
      sync speed data.
  8.  Change: General minor logging improvements.
  9.  Fix: Fixed the problem of the app crashing on some Windows 2000 systems.
  10. New: As well as the three existing tooltip icons, three new ones have
      been added. They are the same as the original three, but with a black
      bar across them. (See the help for examples.) They indicate that the
      router itself is uncontactable.

- 1.2.0.0 (10 January 2007)
  1.  Changed the router IP address to use the default gateway, unless it is
      over-ridden in the config file. Therefore, the value in the config
      file has had its name changed to router_ip_override and can in most
      instances be left blank.
  2.  Changed the logging file to a daily log. This prevents the log file
      growing too big.
  3.  Fixed a bug whereby the sample custom commands were the ones being run
      instead of the "live" ones.
  4.  Fixed a bug whereby if the router was not contactable when a reboot
      attempt was made and there are custom commands, the failure to contact
      the router was not being logged.
  5.  Fixed a bug whereby very rarely a logging entry failed to get written
      to the log file.
  6.  Improved the logging to be more consistent when the router is rebooted.
  7.  Logging now shows when Bounce Sky Router is started.

- 1.1.1.0 (08 January 2007)
  1.  Wrote the help file.
  2.  Add a help option to the context menu.
  3.  Fixed the bug whereby Bounce Sky Router would try to reboot the router
      if the computer it was running on came out of standby / hibernation.
      There is now a 60 second grace period during which time the auto-reboot
      functionality is disabled. This should be enough time for the IP stack
      to restart.

- 1.1.0.0 (23 December 2006)
  1.  Added functionality to run multiple custom commands.
  2.  Added menu option to run the custom commands manually. (They are also
      run after every automatic reboot).
  3.  There is a "boing" sound when an automatic reboot is taking place.
  4.  Changed a few fonts.
  5.  It now checks the Internet for a newer version.
  6.  Added more information to the "How to use" section.

- 1.0.2.0 (16 December 2006)
  1.  Fixed bug of the form showing in Alt-Tab.

- 1.0.1.0 (16 December 2006)
  1.  Added a menu option to get the ADSL user name and password.
  2.  Fixed a bug whereby the menu options didn't always grey out when they
      should.
  3.  Added the stats data to the tooltip. (This was done in the previous
      version, so technically, the bug fix is this readme!)
  4.  Added the system uptime to the tooltip.
  5.  Made one of the potential warnings more meaningful.
  6.  The automatic upgrade now works, so no need to remove the application
      in Add / Remove programs first.

- 1.0.0.0 (14 December 2006)
  1.  Almost complete re-write of the code to make the application
      multithreaded. The result of this is that the user interface should be
      much more responsive and not appear to hang every now and then when
      it's busy.
  2.  More menu options: 2 new "do stuff now" options. (You'll see what I
      mean!)
  3.  Added menu option to view (and edit) the config file in notepad. Note
      that the app needs to be restarted if the config file is edited.
  4.  Made the logging more meaningful.
  5.  Added an option to send a custom command to the router using URL injection
      after the router has been rebooted. The custom command is added to the
      config file. By default, the custom command is to do nothing, but the
      config file has a commented out sample that enabled the telnet daemon.
  6.  Prepared the code for future versions to be upgraded automatically. (For
      now, 0.1.2.0 will have to be uninstalled manually.)

- 0.1.2.0 (29 November 2006):
  1.  Fixed a bug whereby the app prompts for a reboot even after an automatic
      reboot.
  2.  Fixed the bug whereby a manual reboot results in an error stating that
      the router could not be contacted.

- 0.1.1.0 (27 November 2006): 
  1.  Souped up this readme file.
  2.  Added setup icons and banner.
  3.  Added a shortcut to program files.
  4.  Added a customisable timeout. (Maximum value = 30000ms.)
  5.  Sorted out the namespaces.
  6.  Tidied up the code for the assembly title in the About form.
  7.  Changed the default password to sky.
  8.  Changed the Desktop icon.
  9.  More accurate timing of web responses. (Previous version timings
      included entry and exit from try catch statement. D'oh!)
  10. Now checks that the router can be logged into at startup.
  11. Added a "Show Stats" menu option.
  12. Few cosmetic changes.
  13. Added confirmation before rebooting the router.

- 0.0.7.0 (23 November 2006): First version to be published.


Planned Changes:
================
High Priority:
1.  Options set via an options form rather than directly via the config file.

Medium Priority:
2.  Add an option for the log viewer to load up previous and next days.
3.  Customisable log file location.

Low Priority:
4.  Add an option to run at startup. (Only an outside chance of this
    because this can be done by adding a shortcut to the Startup folder. Also,
    I am trying to get away from spattering the registry with my stuff because
    it's just more to clear up at uninstall!)
5.  Other notification methods, such as an email. (Possible in some future
    version.)


Contacts:
=========
PM any emails, suggestions, corrections etc. to my mailbox (mossywell) on the
excellent www.skyuser.co.uk. (See Disclaimer 1!) NOTE: General comments, pats
on the back, offers of beer and so on are welcome on the forum, but in general,
comments, bugs etc should be done via a PM to avoid cluttering the forum with
support issues.


Disclaimers:
============
1. I don't provide technical support for this app as it's freeware. However,
   I can be contacted by PM at www.skyuser.co.uk. PLEASE use PM and not the
   general forum for contacting me.
2. The license agreement in the help file has all the license and disclaimer
   details.
   
   
General Notes:
==============
This topic is now covered in the help file.
   

What it does:
=============
This topic is now covered in the help file.


How to use it:
==============
This topic is now covered in the help file.






