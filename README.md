# geolocatedcheckin
Windows trigger to track checkins by physical location based on GeoCoordinateWatcher.

If you work distributed from different locations, you can use this trigger to track where you were working from. It uses the GeoCoordinateWatcher API and also shows how to automate some simple operations with CmdRunner.

# How to install it
Get the geolocation.exe from releases and then install your trigger as follows:

  ``cm maketrigger after-clientcheckin "add geolocation" "C:\Users\pablo\plasticscm\clienttriggers\geolocation.exe"``
  
And then every single checkin will be tracked (provided your laptop can give you the right location. I tested it on W10 correctly).

## How to set a different "cm"
Internally we use 'bcm' instead of cm. If that's your case too, or if you need to use different Plastic installations, check this:

  ``cm maketrigger after-clientcheckin "add geolocation" "C:\Users\pablo\plasticscm\clienttriggers\geolocation.exe --cm bcm"``
  
## All the options
   ``geolocation.exe help``
   ``geolocation [help] | [--cm command]``
    
    --cm command:           to specify an alternative cm. We use it internally
    
    --attribute attrname:   to specify the attr. 'geoloc' is the default

As you can see, you can also customize the attribute name.

# How it works
        
