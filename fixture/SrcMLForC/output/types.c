<unit language="C">
  <struct>struct <name>test</name><block>{
 <decl_stmt><decl><type><name>int</name></type><name>a</name></decl>;</decl_stmt><decl_stmt><decl><type><name>int</name> *</type><name>b</name></decl>;</decl_stmt><decl_stmt><decl><type><name>int</name> *</type><name><name>c</name><index>[<expr>10</expr>]</index></name></decl>;</decl_stmt><decl_stmt><decl><type><name>int</name> **</type><name><name>d</name><index>[<expr>20</expr>]</index></name></decl>;</decl_stmt>
}</block><decl><name>t1</name></decl>;</struct>
  <decl_stmt>
    <decl>
      <type>
        <name>int</name>
      </type> main<argument_list>(<argument><expr><name>int</name>*<index>[]</index></expr></argument>, <argument><expr><name>int</name>**<index>[<expr>10</expr>]</index></expr></argument>, <argument><expr><name>int</name> *<name><name>c</name><index>[]</index></name></expr></argument>, <argument><expr><name>int</name> *<name>const</name>*<name><name>d</name><index>[<expr>10</expr>]</index></name></expr></argument>)</argument_list></decl>;</decl_stmt>
  <function>
    <type>
      <name>int</name>
    </type>
    <name>main</name>
    <parameter_list>(<param><decl><type><name>int</name> *</type><name><name>a</name><index>[<expr>10</expr>]</index></name></decl></param>)</parameter_list>
    <block>{
 <decl_stmt><decl><type>struct <name>test</name></type><name>t2</name></decl>;</decl_stmt><return>return <expr>0</expr>;</return>
}</block>
  </function>
</unit>