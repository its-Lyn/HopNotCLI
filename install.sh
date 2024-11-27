#1/usr/bin/env bash

if [ "$1" == "--remove" ]; then
	sudo rm /usr/local/bin/hopnot
fi

read -r -p "Before running, make sure you've ran \"dotnet publish -c Release -p:PublishSingleFile=true\". Continue? [Y/n] " RESPONSE
case $RESPONSE in
	[nN])
		exit 0
	;;
esac

sudo mv HopNotCLI/bin/Release/not8.0/linux-x64/publish/hopnot /usr/local/bin
