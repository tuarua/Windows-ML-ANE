package {
import com.tuarua.FreSharp;
import com.tuarua.MLANE;
import com.tuarua.MLEvent;
import com.tuarua.fre.ANEError;

import flash.desktop.NativeApplication;

import flash.display.Bitmap;

import flash.display.Sprite;
import flash.display.StageAlign;
import flash.display.StageScaleMode;
import flash.events.Event;
import flash.events.MouseEvent;
import flash.filesystem.File;
import flash.text.AntiAliasType;
import flash.text.Font;
import flash.text.TextField;
import flash.text.TextFormat;

import views.SimpleButton;

[SWF(width="700", height="700", frameRate="60", backgroundColor="#FFFFFF")]
public class Main extends Sprite {
    private var freSharpANE:FreSharp = new FreSharp();//must create before all others
    [Embed(source="cat.jpg")]
    public static const TestImage:Class;

    public static const FONT:Font = new FiraSansSemiBold();
    private var btnZip:SimpleButton = new SimpleButton("Detect Image");
    private var statusLabel:TextField = new TextField();
    private var ane:MLANE = new MLANE();
    private var testImage:Bitmap = new TestImage() as Bitmap;

    public function Main() {
        stage.align = StageAlign.TOP_LEFT;
        stage.scaleMode = StageScaleMode.NO_SCALE;

        NativeApplication.nativeApplication.addEventListener(Event.EXITING, onExiting);
        testImage.y = 50;
        addChild(testImage);
        ane.addEventListener(MLEvent.RESULT, onANEEvent);
        ane.init();

        btnZip.x = (stage.stageWidth - 200) * 0.5;
        btnZip.y = 10;
        btnZip.addEventListener(MouseEvent.CLICK, onZipClick);
        addChild(btnZip);

        var tf:TextFormat = new TextFormat(Main.FONT.fontName, 13, 0x222222);
        tf.align = "center";

        statusLabel.defaultTextFormat = tf;
        statusLabel.width = stage.stageWidth;
        statusLabel.y = btnZip.y + 50;

        statusLabel.wordWrap = statusLabel.multiline = false;
        statusLabel.selectable = false;
        statusLabel.embedFonts = true;
        statusLabel.antiAliasType = AntiAliasType.ADVANCED;
        statusLabel.sharpness = -100;

        addChild(statusLabel);

    }

    private function onZipClick(event:MouseEvent):void {
        statusLabel.text = "Analyzing image...";
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
        statusLabel.text = event.params;
    }

    private function onExiting(event:Event):void {
        ane.dispose();
        freSharpANE.dispose();
    }
}
}
