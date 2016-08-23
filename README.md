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
Once you install the trigger, each time you checkin it will try to create a attribute/value (attribute is 'geoloc' by default, but you can change it) assigned to the new changeset with the information of your location.

You will see something like this in the attributes view of your changeset:

![Attribute view showing the geoloc entry](https://raw.githubusercontent.com/PlasticSCM/geolocatedcheckin/master/screenshots/attribute-view.png)

As you can see it associates a short name (home in this case) to the location.

And once the attribute is created by the trigger, a new notification will show up on the bottom right corner of your screen as follows:

![Notification of a new geoloc attribute](https://raw.githubusercontent.com/PlasticSCM/geolocatedcheckin/master/screenshots/notification.png)

If the location where you are checking in is new (you never checked in from there), then the trigger will ask you to enter a name for the new location:

![Enter a name for the new geolocation](https://raw.githubusercontent.com/PlasticSCM/geolocatedcheckin/master/screenshots/enter-new-location.png)

The known locations are stored on c:\Users\<your-user-name>\AppData\Local\plastic4\geolocatedcheckins.conf

Every new location is compared with the known ones: if it is closer than 2000 m, then it is considered the same location (you can easily customize this in the code).

# Remarks
It is important to run the trigger as a client-side trigger, otherwise it won't be able to correctly interact with your desktop.
