# vagman - Vagrant Manager
In the past, maybe you have happily downloaded lots of Vagrantfiles to try out new software or ideas. Over time, you probably still use some frequently, but with others, you've forgotten why you even downloaded them. On top of that, you've probably also hit the 'no disk space' issue that's sent you trying to delete those massive files filling up your hard drive, and inevitably found some of your virtual machine disk files. Keen to fix the problem, you deleted a few of those.

Now, you've no idea which Vagrantfiles still have a virtual machine, which virtual machines still have a Vagrantfile, and which virtual machines have now had their disk files deleted.

This script was written for just that reason. Of course, maybe you're actually really well organised, in which case you can just laugh at my lack of organisation and move on to someone's more useful repository instead.

## Technical overview
This project has been written in C# using the beta of .NET 5.0 (CoreClr). It is not compatible with Mono. I am currently running it with the 1.0.0-rc2-20221 CoreClr runtime.

It's been coded and tested on OS X 10.11, and has not been tested (and almost certainly won't run) on non-OS X environments.

It is designed to support VirtualBox VMs only.

## What was the reason for me doing this?
Well, it came from a place of frustration at the chaos of my hard drive.

It also is a Hack Day project, where my excellent employer [Christians Against Poverty](https://capuk.org/) gives us space to experiment with new technologies and ideas. For me, I was keen to experiment with C# and the new .NET CoreClr on OS X. 

And the great news is, if you'd like to join our team, [we're recruiting](https://capuk.org/get-involved/you/join-the-team)!
