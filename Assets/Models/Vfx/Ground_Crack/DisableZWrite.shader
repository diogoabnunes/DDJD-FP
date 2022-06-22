Shader "Custom/DisableZWrite"
{
	SubShader{
		Tags{
			"RenderType" = "Opaque"
        }

		Pass{
			ZWRite Off
        }
	}
}