   M S B A M L   `   `   `  ����  DashboardApp%    DashboardApp.Views.CalendarView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      .��5      � $��10q�6     � RowDefinitions  5	   
   �� 5
      $��* �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��0��6   $��2��6/   ��6C   �� *Events+ �� SelectedItem 6b   �� *SelectedEvent+��5      �� 5      .R�5      �� .+�5      �� $
��Bold"�6=   ��6$   �� *	Summary+6=   �� 5      $
��Wrap=�6A   ��6$   �� *Description+6A   �� 5      ��6$   �� *Start+�� 5      ��6$   �� *End+5      5      5      �� 5    
   $��1��6   $��0��6"    �� Orientation$ Vertical=�62   $��5q�6I   .+�5!      �� ��6   �� *NewEventSummary+ �� UpdateSourceTrigger$ PropertyChanged=��� 5"      ��6   �� *NewEventDescription+$ PropertyChanged=�(  "System.Windows.Controls.DatePicker  5#         SelectedDate 6   �� *NewEventStart+$ PropertyChanged=�  5$       6   �� *NewEventEnd+$ PropertyChanged=�5%      �� 5'   
   $��2��6   $��0��6   $��Load Events��6.   $��5q�6j   ��6D   �� *LoadEventsCommand+6j   �� 5(   
   $��2��6   $��1��6"   $ 
Horizontal=�62   .+�5)      �� $��	Add Event��6   $��5q�6M   ��6)   �� *AddEventCommand+6M   �� 5*      $��Update Event��6   $��5q�6S   ��6,   �� *UpdateEventCommand+6S   �� 5+      $��Delete Event��6   $��5q�6S   ��6,   �� *DeleteEventCommand+6S   5,      5-      5.      