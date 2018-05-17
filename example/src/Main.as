package {
import com.tuarua.MLANE;
import com.tuarua.MLEvent;

import flash.desktop.NativeApplication;

import flash.display.Bitmap;

import flash.display.Sprite;
import flash.events.Event;
import flash.events.MouseEvent;
import flash.filesystem.File;
import flash.text.TextField;

[SWF(width="960", height="640", frameRate="60", backgroundColor="#F1F1F1")]
public class Main extends Sprite {
    [Embed(source="testImage.jpg")]
    public static const TestImage:Class;
    private var textField:TextField = new TextField();
    private var ane:MLANE = new MLANE();
    private var testImage:Bitmap = new TestImage() as Bitmap;

    public function Main() {
        NativeApplication.nativeApplication.addEventListener(Event.EXITING, onExiting);
        textField.y = 10;
        testImage.y = 50;
        addChild(testImage);
        ane.addEventListener(MLEvent.RESULT, onANEEvent);
        ane.init();
        addChild(textField);
        stage.addEventListener(MouseEvent.CLICK, onStageClick);
    }

    private function onStageClick(event:MouseEvent):void {
        textField.text = "Analyzing image";
        ane.predict(testImage.bitmapData, File.applicationDirectory.resolvePath("GoogLeNetPlaces.onnx").nativePath);
    }

    private function onANEEvent(event:MLEvent):void {
        textField.text = event.params;
    }

    private function onExiting(event:Event):void {
        ane.dispose();
    }
}
}
