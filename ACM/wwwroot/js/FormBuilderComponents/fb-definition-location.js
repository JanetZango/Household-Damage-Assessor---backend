(function (global, factory) {
    typeof exports === 'object' && typeof module !== 'undefined' ? module.exports = factory() :
        typeof define === 'function' && define.amd ? define(factory) :
            (global.VueFBDefinitionLocation = factory());
}(this, (function () {
    var index = {
        name: 'fb-definition-location',
        props: ['value', 'iindex', 'cindex', 'sindex', 'iiindex', 'readonly'],
        data: function () {
            return {
                
            };
        },
        computed: {

        },
        watch: {

        },
        created: function () {

        },
        methods: {
            update(key, value) {
                this.$emit('input', _.tap(_.cloneDeep(this.value), v => _.set(v, key, value)));
            },
            subSelectResponseItem(event) {
                this.$emit('select-item', this.value, event, this.iindex, this.cindex, this.sindex, this.iiindex);
            }
        },
        template: `<div class="question" v-if="value.childCode == 'LOCATION'" v-on:dblclick="subSelectResponseItem($event)">
                    <div class="row">
                        <div class="col-md-11 question-heading">
                            {{value.childDescription}} <span v-if="value.properties.isMandatory == true">*</span>
                        </div>
                        <div class="col-md-1 text-center" v-if="readonly == false">
                            <i class="fa fa-pen edititem" :id="'edititem_' + value.formDefinitionItemID" v-on:click="subSelectResponseItem($event)"></i>
                        </div>
                    </div>
                    <div class="row instructions" v-if="value.properties.isInstruction == true">
                        <div class="col-md-12 pad-btm">
                            <span class="instruction" role="button" data-toggle="collapse" :href="'#question_instruction_' + value.formDefinitionItemID" aria-expanded="true"><i class="fa fa-eye"></i> Toggle instructions</span>
                            <div class="collapse in" :id="'question_instruction_' + value.formDefinitionItemID">
                                <div v-html="value.properties.instructionText"></div>
                                <img :src="value.properties.instructionImageBase64" v-if="value.properties.instructionImageBase64 != ''" class="img-responsive" />
                            </div>
                        </div>
                    </div>
                    <div class="row" v-if="value.properties.isTextInput == true">
                        <div class="col-md-6">
                            <input type="text" v-model="value.properties.textValue" @input="update('properties.textValue', $event.target.value)" class="form-control" /><br />
                        </div>
                    </div>
                    <div class="row" v-if="value.properties.isLocationList == true">
                        <div class="col-md-6">
                            <button type="button" class="btn btn-block btn-default" data-toggle="modal" :data-target="'#location_answer_modal_' + value.formDefinitionItemID">
                                Select response
                            </button>
                            <div class="modal fade" :id="'location_answer_modal_' + value.formDefinitionItemID" tabindex="-1" role="dialog">
                                <div class="modal-dialog modal-sm" role="document">
                                    <div class="modal-content" style="border-radius: 5px;">
                                        <div class="modal-body">
                                            <div class="btn-group-vertical full-width" role="group">
                                                <button type="button" v-for="answer in value.properties.responseTypeAnswers" :data-answer-id="answer.formBuilderFormDefinitionLocationResponseTypeAnswerID" class="btn btn-block btn-default">{{answer.description}}</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>`
    };
    return index;
})));