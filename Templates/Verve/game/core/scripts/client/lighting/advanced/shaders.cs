//-----------------------------------------------------------------------------
// Torque
// Copyright GarageGames, LLC 2011
//-----------------------------------------------------------------------------


// Vector Light State
new GFXStateBlockData( AL_VectorLightState )
{
   blendDefined = true;
   blendEnable = true;
   blendSrc = GFXBlendOne;
   blendDest = GFXBlendOne;
   blendOp = GFXBlendOpAdd;
   
   zDefined = true;
   zEnable = false;
   zWriteEnable = false;

   samplersDefined = true;
   samplerStates[0] = SamplerClampPoint;  // G-buffer
   samplerStates[1] = SamplerClampPoint;  // Shadow Map (Do not change this to linear, as all cards can not filter equally.)
   samplerStates[2] = SamplerClampLinear;  // SSAO Mask
   samplerStates[3] = SamplerWrapPoint;   // Random Direction Map
   
   cullDefined = true;
   cullMode = GFXCullNone;
   
   stencilDefined = true;
   stencilEnable = true;
   stencilFailOp = GFXStencilOpKeep;
   stencilZFailOp = GFXStencilOpKeep;
   stencilPassOp = GFXStencilOpKeep;
   stencilFunc = GFXCmpLess;
   stencilRef = 0;
};

// Vector Light Material
new ShaderData( AL_VectorLightShader )
{
   DXVertexShaderFile = "shaders/common/lighting/advanced/farFrustumQuadV.hlsl";
   DXPixelShaderFile  = "shaders/common/lighting/advanced/vectorLightP.hlsl";

   OGLVertexShaderFile = "shaders/common/lighting/advanced/gl/farFrustumQuadV.glsl";
   OGLPixelShaderFile  = "shaders/common/lighting/advanced/gl/vectorLightP.glsl";
   
   pixVersion = 3.0;
};

new CustomMaterial( AL_VectorLightMaterial )
{
   shader = AL_VectorLightShader;
   stateBlock = AL_VectorLightState;
   
   sampler["prePassBuffer"] = "#prepass";
   sampler["ShadowMap"] = "$dynamiclight";
   sampler["ssaoMask"] = "#ssaoMask";
   
   target = "lightinfo";
   
   pixVersion = 3.0;
};

//------------------------------------------------------------------------------

// Convex-geometry light states
new GFXStateBlockData( AL_ConvexLightState )
{
   blendDefined = true;
   blendEnable = true;
   blendSrc = GFXBlendOne;
   blendDest = GFXBlendOne;
   blendOp = GFXBlendOpAdd;
   
   zDefined = true;
   zEnable = true;
   zWriteEnable = false;
   zFunc = GFXCmpGreaterEqual;

   samplersDefined = true;
   samplerStates[0] = SamplerClampPoint;  // G-buffer
   samplerStates[1] = SamplerClampPoint;  // Shadow Map (Do not use linear, these are perspective projections)
   samplerStates[2] = SamplerClampLinear; // Cookie Map   
   samplerStates[3] = SamplerWrapPoint;   // Random Direction Map
   
   cullDefined = true;
   cullMode = GFXCullCW;
   
   stencilDefined = true;
   stencilEnable = true;
   stencilFailOp = GFXStencilOpKeep;
   stencilZFailOp = GFXStencilOpKeep;
   stencilPassOp = GFXStencilOpKeep;
   stencilFunc = GFXCmpLess;
   stencilRef = 0;
};

// Point Light Material
new ShaderData( AL_PointLightShader )
{
   DXVertexShaderFile = "shaders/common/lighting/advanced/convexGeometryV.hlsl";
   DXPixelShaderFile  = "shaders/common/lighting/advanced/pointLightP.hlsl";

   OGLVertexShaderFile = "shaders/common/lighting/advanced/gl/convexGeometryV.glsl";
   OGLPixelShaderFile  = "shaders/common/lighting/advanced/gl/pointLightP.glsl";

   pixVersion = 3.0;
};

new CustomMaterial( AL_PointLightMaterial )
{
   shader = AL_PointLightShader;
   stateBlock = AL_ConvexLightState;
   
   sampler["prePassBuffer"] = "#prepass";
   sampler["shadowMap"] = "$dynamiclight";
   sampler["cookieTex"] = "$dynamiclightmask";
   
   target = "lightinfo";
   
   pixVersion = 3.0;
};

// Spot Light Material
new ShaderData( AL_SpotLightShader )
{
   DXVertexShaderFile = "shaders/common/lighting/advanced/convexGeometryV.hlsl";
   DXPixelShaderFile  = "shaders/common/lighting/advanced/spotLightP.hlsl";

   OGLVertexShaderFile = "shaders/common/lighting/advanced/gl/convexGeometryV.glsl";
   OGLPixelShaderFile  = "shaders/common/lighting/advanced/gl/spotLightP.glsl";
   
   pixVersion = 3.0;
};

new CustomMaterial( AL_SpotLightMaterial )
{
   shader = AL_SpotLightShader;
   stateBlock = AL_ConvexLightState;
   
   sampler["prePassBuffer"] = "#prepass";
   sampler["shadowMap"] = "$dynamiclight";
   sampler["cookieTex"] = "$dynamiclightmask";
   
   target = "lightinfo";
   
   pixVersion = 3.0;
};

/// This material is used for generating prepass 
/// materials for objects that do not have materials.
new Material( AL_DefaultPrePassMaterial )
{
   // We need something in the first pass else it 
   // won't create a proper material instance.  
   //
   // We use color here because some objects may not
   // have texture coords in their vertex format... 
   // for example like terrain.
   //
   diffuseColor[0] = "1 1 1 1";
};

/// This material is used for generating shadow 
/// materials for objects that do not have materials.
new Material( AL_DefaultShadowMaterial )
{
   // We need something in the first pass else it 
   // won't create a proper material instance.  
   //
   // We use color here because some objects may not
   // have texture coords in their vertex format... 
   // for example like terrain.
   //
   diffuseColor[0] = "1 1 1 1";
               
   // This is here mostly for terrain which uses
   // this material to create its shadow material.
   //
   // At sunset/sunrise the sun is looking thru 
   // backsides of the terrain which often are not
   // closed.  By changing the material to be double
   // sided we avoid holes in the shadowed geometry.
   //
   doubleSided = true;
};

// Particle System Point Light Material
new ShaderData( AL_ParticlePointLightShader )
{
   DXVertexShaderFile = "shaders/common/lighting/advanced/particlePointLightV.hlsl";
   DXPixelShaderFile  = "shaders/common/lighting/advanced/particlePointLightP.hlsl";

   OGLVertexShaderFile = "shaders/common/lighting/advanced/gl/convexGeometryV.glsl";
   OGLPixelShaderFile  = "shaders/common/lighting/advanced/gl/pointLightP.glsl";
      
   pixVersion = 3.0;
};

new CustomMaterial( AL_ParticlePointLightMaterial )
{
   shader = AL_ParticlePointLightShader;
   stateBlock = AL_ConvexLightState;
   
   sampler["prePassBuffer"] = "#prepass";
   target = "lightinfo";
   
   pixVersion = 3.0;
};
