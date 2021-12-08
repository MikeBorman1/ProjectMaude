#!/usr/bin/env bash

# Debug stuff
#APPCENTER_BUILD_ID="22"
#APPCENTER_BRANCH="master"

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
        # Example: Change bundle name of an iOS app for production

        echo "Updating version name to $VERSION_NAME in Info.plist"
        INFO_PLIST_FILE=`/usr/bin/find . -name Info.plist`
        echo $INFO_PLIST_FILE
        plutil -replace CFBundleShortVersionString -string $VERSION_NAME $INFO_PLIST_FILE

        echo "File content:"
        cat $INFO_PLIST_FILE
fi
