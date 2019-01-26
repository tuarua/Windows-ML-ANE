package {
import com.tuarua.MLANE;
import com.tuarua.MLEvent;
import com.tuarua.fre.ANEError;

import flash.desktop.NativeApplication;

import flash.display.Bitmap;

import flash.display.Sprite;
import flash.events.Event;
import flash.events.MouseEvent;
import flash.filesystem.File;
import flash.text.TextField;

[SWF(width="700", height="700", frameRate="60", backgroundColor="#FFFFFF")]
public class Main extends Sprite {
    [Embed(source="cat.jpg")]
    public static const TestImage:Class;
    private var textField:TextField = new TextField();
    private var ane:MLANE = new MLANE();
    private var testImage:Bitmap = new TestImage() as Bitmap;

    public function Main() {
        NativeApplication.nativeApplication.addEventListener(Event.EXITING, onExiting);
        textField.x = 10;
        textField.width = 200;
        testImage.y = 50;
        addChild(testImage);
        ane.addEventListener(MLEvent.RESULT, onANEEvent);
        ane.init();
        addChild(textField);
        stage.addEventListener(MouseEvent.CLICK, onStageClick);
    }

    private function onStageClick(event:MouseEvent):void {
        textField.text = "Analyzing image";
        try {
            ane.predict(File.applicationDirectory.resolvePath("cat.jpg").nativePath,
                    File.applicationDirectory.resolvePath("SqueezeNet.onnx").nativePath);
        } catch (e:ANEError) {
            trace(e);
            trace(e.message);
            trace(e.getStackTrace());
        }

    }

    private function onANEEvent(event:MLEvent):void {
        textField.text = event.params;
    }

    private function onExiting(event:Event):void {
        ane.dispose();
    }
}
}
