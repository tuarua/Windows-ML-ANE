package com.tuarua {
import flash.events.Event;

public class MLEvent extends Event {
    public static const RESULT:String = "MLANE.OnModelResult";
    public var params:*;
    public function MLEvent(type:String, params:* = null, bubbles:Boolean = false, cancelable:Boolean = false) {
        super(type, bubbles, cancelable);
        this.params = params;
    }
    public override function clone():Event {
        return new MLEvent(type, this.params, bubbles, cancelable);
    }

    public override function toString():String {
        return formatToString("MLEvent", "params", "type", "bubbles", "cancelable");
    }
}
}
