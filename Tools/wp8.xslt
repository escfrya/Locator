<?xml version="1.0"?>
<xsl:stylesheet exclude-result-prefixes="prj" version="1.0" 
  xmlns="http://schemas.microsoft.com/developer/msbuild/2003" 
  xmlns:prj="http://schemas.microsoft.com/developer/msbuild/2003" 
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  
  <xsl:output encoding="UTF-8" indent="yes" method="xml" version="1.0"/>
  
  <xsl:template match="@*|node()">
    <xsl:copy>
      <xsl:apply-templates select="@*|node()"/>
    </xsl:copy>
  </xsl:template>
  
  <xsl:template match="comment()"/>
  
  <xsl:template match="prj:ProjectGuid">
    <xsl:copy>
      <xsl:apply-templates select="@*|node()"/>
    </xsl:copy>
    <ProjectTypeGuids>{C089C8C0-30E0-4E22-80C0-CE093F111A43};{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}</ProjectTypeGuids>
	<TargetFrameworkIdentifier>WindowsPhone</TargetFrameworkIdentifier>
    <TargetFrameworkVersion>v8.0</TargetFrameworkVersion>
  </xsl:template>
  
  <xsl:template match="prj:PreBuildEvent"/>
  <xsl:template match="prj:PostBuildEvent"/>
  <xsl:template match="prj:TargetFrameworkVersion"/>
  
  <xsl:template match="prj:PropertyGroup[@Condition[contains(.,'Debug')]]/prj:DefineConstants">
    <DefineConstants>DEBUG;TRACE;SILVERLIGHT;WINDOWS_PHONE</DefineConstants>
  </xsl:template>
  
  <xsl:template match="prj:PropertyGroup[@Condition[contains(.,'Release')]]/prj:DefineConstants">
    <DefineConstants>TRACE;SILVERLIGHT;WINDOWS_PHONE</DefineConstants>
  </xsl:template>
  
  <xsl:template match="prj:ProjectReference/@Include">
    <xsl:attribute name="Include">
      <xsl:call-template name="replace">
        <xsl:with-param name="input" select="."/>
        <xsl:with-param name="from" select="'.csproj'"/>
        <xsl:with-param name="to" select="'.WP8.csproj'"/>
      </xsl:call-template>
    </xsl:attribute>
  </xsl:template>
  
  <xsl:template match="prj:ProjectReference/prj:Name">
    <Name>
      <xsl:value-of select="."/>
      <xsl:text>.WP8</xsl:text>
    </Name>
  </xsl:template>
  
  <xsl:template name="replace">
    <xsl:param name="input"/>
    <xsl:param name="from"/>
    <xsl:param name="to"/>
    <xsl:choose>
      <xsl:when test="contains($input, $from)">
        <xsl:value-of select="substring-before($input, $from)"/>
        <xsl:value-of select="$to"/>
        <xsl:call-template name="replace">
          <xsl:with-param name="input" select="substring-after($input, $from)"/>
          <xsl:with-param name="from" select="$from"/>
          <xsl:with-param name="to" select="$to"/>
        </xsl:call-template>
      </xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="$input"/>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  
  <xsl:template match="prj:ItemGroup/prj:Reference">
  </xsl:template>
  
  <xsl:template match="prj:Import"/>
  <xsl:template match="/*">
	<xsl:copy>
      <xsl:apply-templates select="@*|node()"/>
	  <Import Project="$(MSBuildExtensionsPath)\Microsoft\$(TargetFrameworkIdentifier)\$(TargetFrameworkVersion)\Microsoft.$(TargetFrameworkIdentifier).$(TargetFrameworkVersion).Overrides.targets" />
	  <Import Project="$(MSBuildExtensionsPath)\Microsoft\$(TargetFrameworkIdentifier)\$(TargetFrameworkVersion)\Microsoft.$(TargetFrameworkIdentifier).CSharp.targets" />
    </xsl:copy>
  </xsl:template>
</xsl:stylesheet>