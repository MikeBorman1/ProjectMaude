#!/usr/bin/env bash

#debug stuff
#APPCENTER_BRANCH="master"
#APPCENTER_BUILD_ID="22"

if [ -z "$MAJOR_VERSION" ]
then
    echo "You need define the MAJOR_VERSION variable in App Center"
    exit
fi

if [ -z "$MINOR_VERSION" ]
then
    echo "You need define the MINOR_VERSION variable in App Center"
    exit
fi

VERSION_NAME="$MAJOR_VERSION.$MINOR_VERSION.$APPCENTER_BUILD_ID"

if [ "$APPCENTER_BRANCH" = "master" ];
then
        echo "Updating version name to $VERSION_NAME in AndroidManifest.xml"
        ANDROID_MANIFEST_FILE=`/usr/bin/find . -name AndroidManifest.xml`
        echo $ANDROID_MANIFEST_FILE

        sed -i s/versionName=\"[0-9.]*\"/versionName=\"$VERSION_NAME\"/g $ANDROID_MANIFEST_FILE
        #/usr/bin/find . -name -exec /bin/sed -i s/versionName="[0-9.]*"/versionName="$VERSION_NAME"/g $ANDROID_MANIFEST_FILE {};
        echo "File content:"
        cat $ANDROID_MANIFEST_FILE
fi
