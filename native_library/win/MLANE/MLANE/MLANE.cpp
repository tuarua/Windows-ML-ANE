#include "MLANE.h"
#include "FreSharpBridge.h"

extern "C" {
	CONTEXT_INIT(TRCML) {
		FREBRIDGE_INIT

		/**************************************************************************/
		/******* MAKE SURE TO ADD FUNCTIONS HERE THE SAME AS MAINCONTROLLER.CS *****/
		/**************************************************************************/

		static FRENamedFunction extensionFunctions[] = {
			 MAP_FUNCTION(init)
			,MAP_FUNCTION(predict)
		};

		SET_FUNCTIONS
	}

	CONTEXT_FIN(TRCML) {
		FreSharpBridge::GetController()->OnFinalize();
	}
	EXTENSION_INIT(TRCML)
	EXTENSION_FIN(TRCML)

}

