=== Credits and Contact ===
Alexander Birke alex@alexanderbirke.dk
Copyright Rumpus Animation www.rumpusanimation.com

=== Purpose ===
UniGayo is a semi procedural lipsync solution that lets you create sequences of phonemes (mouth shapes) based on recorded dialogue and export it into Unity. 

The project is based on Papagayo, another open source project meant specifically for Lost Marple's Anime Studio. UniGayo has been made to integrate with Unity but it should be fairly easy to integrate the output into other programs as well. The project consists of the phoneme editor written in python that is used to match up the text to the audio and various unity editor scripts and components that lets you integrate the lipsync data into your project.

=== License(s) ===
UniGayo is licensed under two licences:
The unity code is licensed under the MIT license
The python code is licensed under the GPLv2 General Public License

The choice of licences is based on the fact that the python part of UniGayo is based on Papagayo which is under GPL. However in order to make it easier for others to integrate UniGayo into your unity projects we choose to release that part under MIT. 

=== How to work with the code ===
To work with the python code you will need:

python 2.7
wxpython
wxglade (not required but allows you to edit the GUI layout more easily

=== Good Resources ===
explanation for the phonemes used to define words in the python program
http://www.speech.cs.cmu.edu/cgi-bin/cmudict
http://en.wikipedia.org/wiki/Arpabet

=== Thanks to ===
Mike Clifton for the initial version of Papagayo and Benjamin Lau for the 1.08 version that fixed the audio issues